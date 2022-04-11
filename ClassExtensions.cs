using System;
using System.Collections.Generic;
using System.Linq;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.GameInput;

namespace AvalonTesting;

public static class ClassExtensions
{
    /// <summary>
    ///     Removes the specified index of a List of int - used for Golem dropping 2 items from its 11-drop loot table.
    /// </summary>
    /// <param name="list">The List of int.</param>
    /// <param name="index">The index.</param>
    /// <returns>Returns the item ID of the removed index.</returns>
    public static int RemoveAtIndex(this List<int> list, int index)
    {
        int item = list[index];
        list.RemoveAt(index);
        return item;
    }
    /// <summary>
    /// Helper method for opening a chest. This method exists in vanilla, but is private.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="x">X coordinate of the chest.</param>
    /// <param name="y">Y coordinate of the chest.</param>
    /// <param name="newChest">The chest index.</param>
    public static void OpenChest(this Player p, int x, int y, int newChest)
    {
        if (p.chest != -1 && Main.myPlayer == p.whoAmI)
        {
            for (int i = 0; i < 40; i++)
            {
                ItemSlot.SetGlow(i, -1f, chest: true);
            }
        }
        p.chest = newChest;
        Main.playerInventory = true;
        UILinkPointNavigator.ForceMovementCooldown(120);
        if (PlayerInput.GrappleAndInteractAreShared)
        {
            PlayerInput.Triggers.JustPressed.Grapple = false;
        }
        Main.recBigList = false;
        p.chestX = x;
        p.chestY = y;
    }
    /// <summary>
    ///     Rotate a Vector2.
    /// </summary>
    /// <param name="spinningpoint">The origin.</param>
    /// <param name="radians">The angle in radians to rotate the Vector2 by.</param>
    /// <param name="center"></param>
    /// <returns>The rotated Vector2.</returns>
    public static Vector2 Rotate(this Vector2 spinningpoint, double radians, Vector2 center = default)
    {
        float num = (float)Math.Cos(radians);
        float num2 = (float)Math.Sin(radians);
        Vector2 vector = spinningpoint - center;
        Vector2 result = center;
        result.X += (vector.X * num) - (vector.Y * num2);
        result.Y += (vector.X * num2) + (vector.Y * num);
        return result;
    }

    public static Rectangle NewRectVector2(Vector2 v, Vector2 wH)
    {
        return new Rectangle((int)v.X, (int)v.Y, (int)wH.X, (int)wH.Y);
    }

    /// <summary>
    ///     Checks if the current player has an item in their armor/accessory slots.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="type">The item ID to check.</param>
    /// <returns>Whether or not the item is found.</returns>
    public static bool HasItemInArmor(this Player p, int type)
    {
        for (int i = 0; i < p.armor.Length; i++)
        {
            if (type == p.armor[i].type)
            {
                return true;
            }
        }

        return false;
    }

    public static int GetItemOre(this AvalonTestingWorld.RhodiumVariant osmiumVariant)
    {
        switch (osmiumVariant)
        {
            case AvalonTestingWorld.RhodiumVariant.osmium:
                return ModContent.ItemType<OsmiumOre>();
            case AvalonTestingWorld.RhodiumVariant.rhodium:
                return ModContent.ItemType<RhodiumOre>();
            case AvalonTestingWorld.RhodiumVariant.iridium:
                return ModContent.ItemType<IridiumOre>();
            default:
                return -1;
        }
    }

    public static bool InPillarZone(this Player p)
    {
        if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
        {
            return p.ZoneTowerNebula;
        }

        return true;
    }

    public static int FindClosestNPC(this Entity entity, float maxDistance, Func<NPC, bool> invalidNPCPredicate)
    {
        int closest = -1;
        float lastDistance = maxDistance;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC npc = Main.npc[i];
            if (invalidNPCPredicate.Invoke(npc))
            {
                continue;
            }

            if (Vector2.Distance(entity.Center, npc.Center) < lastDistance)
            {
                lastDistance = Vector2.Distance(entity.Center, npc.Center);
                closest = i;
            }
        }

        return closest;
    }

    /// <summary>
    ///     Helper method for Vampire Teeth and Blah's Knives lifesteal.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="dmg">The damage to use in the lifesteal calculation.</param>
    /// <param name="position">The position to spawn the lifesteal projectile at.</param>
    public static void VampireHeal(this Player p, int dmg, Vector2 position)
    {
        float num = dmg * 0.075f;
        if ((int)num == 0)
        {
            return;
        }

        if (p.lifeSteal <= 0f)
        {
            return;
        }

        p.lifeSteal -= num;
        int num2 = p.whoAmI;
        Projectile.NewProjectile(p.GetProjectileSource_Accessory(new Item(ModContent.ItemType<VampireTeeth>())),
            position.X, position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, p.whoAmI, num2, num);
    }

    /// <summary>
    ///     Helper method for checking if the current item is an armor piece - used for armor prefixes.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Whether or not the item is an armor piece.</returns>
    public static bool IsArmor(this Item item)
    {
        if (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1)
        {
            return !item.vanity;
        }

        return false;
    }

    public static ExxoPlayer Avalon(this Player p)
    {
        return p.GetModPlayer<ExxoPlayer>();
    }

    public static ExxoBiomePlayer AvalonBiome(this Player p)
    {
        return p.GetModPlayer<ExxoBiomePlayer>();
    }

    public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType)
    {
        return ModContent.Request<Texture2D>(texturedType.Texture);
    }

    public static Rectangle GetDims(this ModTexturedType texturedType)
    {
        return Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();
    }

    /// <summary>
    ///     A helper method to check if the given Player is touching the ground.
    /// </summary>
    /// <param name="player"></param>
    /// <returns>True if the player is touching the ground, false otherwise.</returns>
    public static bool IsOnGround(this Player player)
    {
        return (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].HasTile &&
                Main.tileSolid
                    [Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].TileType]) ||
               (Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].HasTile &&
                Main.tileSolid[
                    Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].TileType] &&
                player.velocity.Y == 0f);
    }

    public static bool Exists(this Item item)
    {
        return item.type > ItemID.None && item.stack > 0;
    }

    public static TagCompound Save<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
    {
        TKey[] keys = dictionary.Keys.ToArray();
        TValue[] values = dictionary.Values.ToArray();
        var tag = new TagCompound();
        tag.Set("keys", keys);
        tag.Set("values", values);
        return tag;
    }

    public static void Load<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TagCompound tag)
    {
        if (tag.ContainsKey("keys") && tag.ContainsKey("values"))
        {
            TKey[] keys = tag.Get<TKey[]>("keys");
            TValue[] values = tag.Get<TValue[]>("values");

            for (int i = 0; i < keys.Length; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
    }

    public static bool DrawFishingLine(this Projectile projectile, int fishingRodType, Color lineColor,
                                       int xPositionAdditive = 45, float yPositionAdditive = 35f)
    {
        Player player = Main.player[projectile.owner];
        Item heldItem = player.HeldItem;
        if (!projectile.bobber || heldItem.holdStyle <= 0)
        {
            return false;
        }

        float playerMountedXCenter = player.MountedCenter.X;
        float y = player.MountedCenter.Y;
        y += player.gfxOffY;
        float gravDir = player.gravDir;
        if (heldItem.type == fishingRodType)
        {
            playerMountedXCenter += xPositionAdditive * player.direction;
            if (player.direction < 0)
            {
                playerMountedXCenter -= 13f;
            }

            y -= yPositionAdditive * gravDir;
        }

        if (gravDir == -1f)
        {
            y -= 12f;
        }

        var playerPosModified = new Vector2(playerMountedXCenter, y);
        playerPosModified = player.RotatedRelativePoint(playerPosModified + new Vector2(8f)) - new Vector2(8f);
        Vector2 vector2 = projectile.Center - playerPosModified;
        bool flag = true;
        if (vector2.X == 0f && vector2.Y == 0f)
        {
            return false;
        }

        float num2 = vector2.Length();
        num2 = 12f / num2;
        vector2.X *= num2;
        vector2.Y *= num2;
        playerPosModified -= vector2;
        vector2 = projectile.Center - playerPosModified;
        while (flag)
        {
            float num3 = 12f;
            float num4 = vector2.Length();
            if (float.IsNaN(num4) || float.IsNaN(num4))
            {
                break;
            }

            if (num4 < 20f)
            {
                num3 = num4 - 8f;
                flag = false;
            }

            num4 = 12f / num4;
            vector2.X *= num4;
            vector2.Y *= num4;
            playerPosModified += vector2;
            vector2 = projectile.Center - playerPosModified;
            if (num4 > 12f)
            {
                float num5 = 0.3f;
                float num6 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                if (num6 > 16f)
                {
                    num6 = 16f;
                }

                num6 = 1f - (num6 / 16f);
                num5 *= num6;
                num6 = num4 / 80f;
                if (num6 > 1f)
                {
                    num6 = 1f;
                }

                num5 *= num6;
                if (num5 < 0f)
                {
                    num5 = 0f;
                }

                num6 = 1f - (projectile.localAI[0] / 100f);
                num5 *= num6;
                if (vector2.Y > 0f)
                {
                    vector2.Y *= 1f + num5;
                    vector2.X *= 1f - num5;
                }
                else
                {
                    num6 = Math.Abs(projectile.velocity.X) / 3f;
                    if (num6 > 1f)
                    {
                        num6 = 1f;
                    }

                    num6 -= 0.5f;
                    num5 *= num6;
                    if (num5 > 0f)
                    {
                        num5 *= 2f;
                    }

                    vector2.Y *= 1f + num5;
                    vector2.X *= 1f - num5;
                }
            }

            Color color = Lighting.GetColor((int)playerPosModified.X / 16, (int)playerPosModified.Y / 16, lineColor);
            float rotation = vector2.ToRotation() - ((float)Math.PI / 2f);
            Main.spriteBatch.Draw(TextureAssets.FishingLine.Value,
                new Vector2(
                    playerPosModified.X - Main.screenPosition.X + (TextureAssets.FishingLine.Value.Width * 0.5f),
                    playerPosModified.Y - Main.screenPosition.Y + (TextureAssets.FishingLine.Value.Height * 0.5f)),
                new Rectangle(0, 0, TextureAssets.FishingLine.Value.Width, (int)num3), color, rotation,
                new Vector2(TextureAssets.FishingLine.Value.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
        }

        return false;
    }

    public static LeadingConditionRule HideFromBestiary(this IItemDropRule itemDropRule)
    {
        var conditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
        conditionRule.OnFailedConditions(itemDropRule, true);
        return conditionRule;
    }
}
