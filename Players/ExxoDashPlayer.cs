using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Players;

/// <summary>
///     An advanced player system that allows simultaneous and seamless dashing for different items,
/// </summary>
public class ExxoDashPlayer : ModPlayer
{
    public enum DashDirection
    {
        Down,
        Up,
        Right,
        Left
    }

    private static readonly Dictionary<int, DashInfo> RegisteredDashes = new();
    private readonly Dictionary<int, DashData> activeDashes = new();
    private readonly Queue<ModItem> dashCheckQueue = new();

    public override bool CloneNewInstances => false;

    /// <summary>
    ///     Register a new dash item and provide metadata for the dash configuration
    /// </summary>
    /// <param name="sourceItemId">The ItemID of the item</param>
    /// <param name="dashInfo">The dash configuration</param>
    public static void RegisterNewDashItem(int sourceItemId, DashInfo dashInfo)
    {
        if (!dashInfo.IsValid)
        {
            AvalonTesting.Mod.Logger.Error("Invalid DashInfo provided, please use the constructor method!");
        }

        RegisteredDashes.Add(sourceItemId, dashInfo);
    }

    /// <summary>
    ///     Queues an item to be checked for dashing the next update
    /// </summary>
    /// <param name="sourceItem">The current ModItem instance</param>
    public void QueueDashEffect(ModItem sourceItem)
    {
        if (Player.whoAmI == Main.myPlayer && !activeDashes.ContainsKey(sourceItem.Type))
        {
            dashCheckQueue.Enqueue(sourceItem);
        }
    }

    public override void ResetEffects()
    {
        if (Player.whoAmI != Main.myPlayer)
        {
            return;
        }

        DashDirection? direction = null;
        if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[(int)DashDirection.Down] < 15)
        {
            direction = DashDirection.Down;
        }
        else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[(int)DashDirection.Up] < 15)
        {
            direction = DashDirection.Up;
        }
        else if (Player.controlRight && Player.releaseRight &&
                 Player.doubleTapCardinalTimer[(int)DashDirection.Right] < 15)
        {
            direction = DashDirection.Right;
        }
        else if (Player.controlLeft && Player.releaseLeft &&
                 Player.doubleTapCardinalTimer[(int)DashDirection.Left] < 15)
        {
            direction = DashDirection.Left;
        }

        if (direction != null && CanUseDash())
        {
            for (int i = 0; i < dashCheckQueue.Count; i++)
            {
                ModItem sourceItem = dashCheckQueue.Dequeue();
                DashInfo dashInfo = RegisteredDashes[sourceItem.Type];
                if (dashInfo.Directions.Contains((DashDirection)direction))
                {
                    activeDashes.Add(sourceItem.Type,
                        new DashData((DashDirection)direction, sourceItem.Item.damage, sourceItem.Item.knockBack));
                    SyncDashPlayer(sourceItem.Type);

                    break;
                }
            }
        }

        dashCheckQueue.Clear();
    }

    public void SyncDashPlayer(int key, int ignoreClient = -1)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            return;
        }

        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)AvalonTesting.MessageType.ExxoDashPlayerSyncActiveDash);
        packet.Write((byte)Player.whoAmI);
        packet.Write(key);
        packet.Write((byte)activeDashes[key].Direction);
        // Dont need to send damage or knockback
        packet.Send(ignoreClient: ignoreClient);
    }

    public void HandleSyncDashPlayer(int key, BinaryReader reader)
    {
        activeDashes.Add(key, new DashData((DashDirection)reader.ReadByte(), 0, 0));
    }

    public void SyncRemoveDashPlayer(int key, int ignoreClient = -1)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            return;
        }

        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)AvalonTesting.MessageType.ExxoDashPlayerSyncRemoveDash);
        packet.Write((byte)Player.whoAmI);
        packet.Write(key);
        packet.Send(ignoreClient: ignoreClient);
    }

    public void HandleSyncRemoveDashPlayer(int key)
    {
        if (activeDashes.ContainsKey(key))
        {
            activeDashes.Remove(key);
        }
    }

    public override void PreUpdateMovement()
    {
        foreach (int dashKey in activeDashes.Keys)
        {
            DashInfo dashInfo = RegisteredDashes[dashKey];
            DashData dashData = activeDashes[dashKey];

            if (dashData.Delay == 0)
            {
                if (Player.whoAmI == Main.myPlayer)
                {
                    Vector2 newVelocity = Player.velocity;
                    switch (dashData.Direction)
                    {
                        // Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
                        case DashDirection.Up when Player.velocity.Y > -dashInfo.Velocity:
                        case DashDirection.Down when Player.velocity.Y < dashInfo.Velocity:
                        {
                            float dashDirection = dashData.Direction == DashDirection.Down ? 1 : -1.3f;
                            newVelocity.Y = dashDirection * dashInfo.Velocity;
                            break;
                        }
                        case DashDirection.Left when Player.velocity.X > -dashInfo.Velocity:
                        case DashDirection.Right when Player.velocity.X < dashInfo.Velocity:
                        {
                            float dashDirection = dashData.Direction == DashDirection.Right ? 1 : -1;
                            newVelocity.X = dashDirection * dashInfo.Velocity;
                            break;
                        }
                        default:
                            // Not fast enough
                            activeDashes.Remove(dashKey);
                            SyncRemoveDashPlayer(dashKey);
                            continue;
                    }

                    Player.velocity = newVelocity;
                }

                dashData.Timer = dashInfo.Duration;
                dashData.Delay = dashInfo.Cooldown;
            }

            if (dashData.Timer > 0)
            {
                Player.eocDash = dashData.Timer;
                Player.armorEffectDrawShadowEOCShield = true;

                if (!dashData.HasHitEnemy)
                {
                    int npcIndex = CheckHitCollision();
                    if (npcIndex != -1)
                    {
                        NPC npc = Main.npc[CheckHitCollision()];
                        float dashDamage = dashData.Damage * Player.GetDamage<GenericDamageClass>()
                            .CombineWith(Player.GetDamage<MeleeDamageClass>());
                        float dashKnockBack = dashData.Knockback * Player
                            .GetKnockback<GenericDamageClass>().CombineWith(Player.GetKnockback<MeleeDamageClass>());
                        bool doesCriticalHit = Main.rand.Next(100) < Player.GetCritChance<GenericDamageClass>() +
                            Player.GetCritChance<MeleeDamageClass>();
                        int damageDirection = Player.velocity.X switch
                        {
                            < 0f => -1,
                            > 0f => 1,
                            _ => Player.direction
                        };

                        if (Player.whoAmI == Main.myPlayer)
                        {
                            Player.ApplyDamageToNPC(npc, (int)dashDamage, dashKnockBack, damageDirection,
                                doesCriticalHit);
                        }

                        dashData.Timer = 11;
                        dashData.Delay = 31;
                        dashData.HasHitEnemy = true;
                        Player.velocity.X = -damageDirection * 9;
                        Player.velocity.Y = -4f;
                        Player.GiveImmuneTimeForCollisionAttack(4);
                    }
                }

                dashData.Timer--;
            }

            dashData.Delay--;
            if (dashData.Delay == 0)
            {
                activeDashes.Remove(dashKey);
                if (Player.whoAmI == Main.myPlayer)
                {
                    SyncRemoveDashPlayer(dashKey);
                }
            }
        }
    }

    private bool CanUseDash()
    {
        return Player.dashType == 0 // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
               && !Player.setSolar // player isn't wearing solar armor
               && !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
    }

    private int CheckHitCollision()
    {
        var rectangle = new Rectangle((int)(Player.position.X + (Player.velocity.X * 0.5) - 4.0),
            (int)(Player.position.Y + (Player.velocity.Y * 0.5) - 4.0), Player.width + 8,
            Player.height + 8);

        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.active || npc.dontTakeDamage || npc.friendly || (npc.aiStyle == 112 && !(npc.ai[2] <= 1f)) ||
                !Player.CanNPCBeHitByPlayerOrPlayerProjectile(npc))
            {
                continue;
            }

            Rectangle rect = npc.getRect();
            if (rectangle.Intersects(rect) && (npc.noTileCollide || Player.CanHit(npc)))
            {
                return i;
            }
        }

        return -1;
    }

    private class DashData
    {
        public readonly int Damage;
        public readonly DashDirection Direction;
        public readonly float Knockback;
        public int Delay;
        public bool HasHitEnemy;
        public int Timer;

        public DashData(DashDirection direction, int damage, float knockback)
        {
            Direction = direction;
            Damage = damage;
            Knockback = knockback;
        }
    }

    /// <summary>
    ///     Outlines the metadata that is stored for a specific item's dash
    /// </summary>
    public readonly struct DashInfo
    {
        public DashInfo(DashDirection[] directions, int cooldown, int duration, int velocity)
        {
            Directions = directions;
            Cooldown = cooldown;
            Duration = duration;
            Velocity = velocity;
            IsValid = true;
        }

        public readonly DashDirection[] Directions;
        public readonly int Cooldown;
        public readonly int Duration;
        public readonly float Velocity;
        public readonly bool IsValid;
    }
}
