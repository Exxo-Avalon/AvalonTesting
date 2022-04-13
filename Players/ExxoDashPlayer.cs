using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
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

    public override bool CloneNewInstances => false;

    private static readonly Dictionary<int, DashInfo> RegisteredDashes = new();
    private readonly Dictionary<int, DashData> activeDashes = new();
    private readonly Queue<ModItem> dashCheckQueue = new();

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
        if (!activeDashes.ContainsKey(sourceItem.Type))
        {
            dashCheckQueue.Enqueue(sourceItem);
        }
    }

    public override void ResetEffects()
    {
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
                    activeDashes.Add(sourceItem.Type, new DashData((DashDirection)direction, sourceItem));
                    break;
                }
            }
        }

        dashCheckQueue.Clear();
    }

    public override void PreUpdateMovement()
    {
        foreach (int dashKey in activeDashes.Keys)
        {
            DashInfo dashInfo = RegisteredDashes[dashKey];
            DashData dashData = activeDashes[dashKey];

            if (dashData.Delay == 0)
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
                        continue;
                }

                dashData.Timer = dashInfo.Duration;
                dashData.Delay = dashInfo.Cooldown;

                Player.velocity = newVelocity;
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
                        float dashDamage = dashData.SourceItem.Item.damage * Player.GetDamage<GenericDamageClass>()
                            .CombineWith(Player.GetDamage<MeleeDamageClass>());
                        float dashKnockBack = dashData.SourceItem.Item.knockBack * Player
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
        public readonly DashDirection Direction;
        public readonly ModItem SourceItem;
        public int Delay;
        public bool HasHitEnemy;
        public int Timer;

        public DashData(DashDirection direction, ModItem sourceItem)
        {
            Direction = direction;
            SourceItem = sourceItem;
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
