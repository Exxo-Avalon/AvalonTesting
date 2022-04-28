using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AvalonTesting.Items.Accessories;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AvalonTesting;

public static class ClassExtensions
{
    private static readonly Action<Player, int, int, int> OpenChestAction = CacheOpenChestAction();

    private static Action<Player, int, int, int> CacheOpenChestAction()
    {
        ParameterExpression paramPlayer = Expression.Parameter(typeof(Player));
        ParameterExpression paramX = Expression.Parameter(typeof(int));
        ParameterExpression paramY = Expression.Parameter(typeof(int));
        ParameterExpression paramNewChest = Expression.Parameter(typeof(int));
        MethodInfo methodInfo = typeof(Player).GetMethod("OpenChest", BindingFlags.NonPublic | BindingFlags.Instance,
            new[] {typeof(int), typeof(int), typeof(int)})!;
        return Expression
            .Lambda<Action<Player, int, int, int>>(
                Expression.Call(
                    paramPlayer,
                    methodInfo,
                    new Expression[] {paramX, paramY, paramNewChest}),
                paramPlayer, paramX, paramY, paramNewChest).Compile();
    }

    /// <summary>
    ///     This is calls a dynamically compiled expression which calls the vanilla private OpenChest method.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="x">X coordinate of the chest.</param>
    /// <param name="y">Y coordinate of the chest.</param>
    /// <param name="newChest">The chest index.</param>
    public static void OpenChest(this Player player, int x, int y, int newChest)
    {
        OpenChestAction.Invoke(player, x, y, newChest);
    }


    public static void active(this Tile t, bool a)
    {
        t.HasTile = a;
    }

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

    public static bool InPillarZone(this Player p)
    {
        if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
        {
            return p.ZoneTowerNebula;
        }

        return true;
    }

    // Used to draw float coordinates to nearest pixel coordinates to avoid blurry rendering of textures
    public static Vector2 ToNearestPixel(this Vector2 vector)
    {
        return new Vector2((int)vector.X, (int)vector.Y);
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
        Projectile.NewProjectile(p.GetSource_Accessory(new Item(ModContent.ItemType<VampireTeeth>())),
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

    public static LeadingConditionRule HideFromBestiary(this IItemDropRule itemDropRule)
    {
        var conditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
        conditionRule.OnFailedConditions(itemDropRule, true);
        return conditionRule;
    }
}
