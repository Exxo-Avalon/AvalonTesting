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

    public static void Active(this Tile t, bool a) => t.HasTile = a;

    public static ExxoPlayer Avalon(this Player p) => p.GetModPlayer<ExxoPlayer>();

    public static ExxoBiomePlayer AvalonBiome(this Player p) => p.GetModPlayer<ExxoBiomePlayer>();

    public static bool Exists(this Item item) => item.type > ItemID.None && item.stack > 0;

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

    public static Rectangle GetDims(this ModTexturedType texturedType) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : texturedType.GetTexture().Frame();

    public static Rectangle GetDims(this ModItem modItem) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modItem.GetTexture().Frame();

    public static Rectangle GetDims(this ModProjectile modProjectile) =>
        Main.netMode == NetmodeID.Server ? Rectangle.Empty : modProjectile.GetTexture().Frame();

    public static Asset<Texture2D> GetTexture(this ModTexturedType texturedType) =>
        ModContent.Request<Texture2D>(texturedType.Texture);

    public static Asset<Texture2D> GetTexture(this ModItem modItem) =>
        ModContent.Request<Texture2D>(modItem.Texture);

    public static Asset<Texture2D> GetTexture(this ModProjectile modProjectile) =>
        ModContent.Request<Texture2D>(modProjectile.Texture);

    /// <summary>
    ///     Checks if the current player has an item in their armor/accessory slots.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="type">The item ID to check.</param>
    /// <returns>Whether or not the item is found.</returns>
    public static bool HasItemInArmor(this Player p, int type) => p.armor.Any(t => type == t.type);

    public static LeadingConditionRule HideFromBestiary(this IItemDropRule itemDropRule)
    {
        var conditionRule = new LeadingConditionRule(new Conditions.NeverTrue());
        conditionRule.OnFailedConditions(itemDropRule, true);
        return conditionRule;
    }

    public static bool InPillarZone(this Player p)
    {
        if (!p.ZoneTowerStardust && !p.ZoneTowerVortex && !p.ZoneTowerSolar)
        {
            return p.ZoneTowerNebula;
        }

        return true;
    }

    /// <summary>
    ///     Helper method for checking if the current item is an armor piece - used for armor prefixes.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Whether or not the item is an armor piece.</returns>
    public static bool IsArmor(this Item item) =>
        (item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity;

    /// <summary>
    ///     A helper method to check if the given Player is touching the ground.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>True if the player is touching the ground, false otherwise.</returns>
    public static bool IsOnGround(this Player player) =>
        (Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f), (int)(player.position.Y / 16f) + 3].TileType]) ||
        (Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].HasTile &&
         Main.tileSolid[
             Main.tile[(int)(player.position.X / 16f) + 1, (int)(player.position.Y / 16f) + 3].TileType] &&
         player.velocity.Y == 0f);

    public static void Load<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TagCompound tag)
        where TKey : notnull
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

    public static Rectangle NewRectVector2(Vector2 v, Vector2 wH) => new((int)v.X, (int)v.Y, (int)wH.X, (int)wH.Y);

    /// <summary>
    ///     This is calls a dynamically compiled expression which calls the vanilla private OpenChest method.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="x">X coordinate of the chest.</param>
    /// <param name="y">Y coordinate of the chest.</param>
    /// <param name="newChest">The chest index.</param>
    public static void OpenChest(this Player player, int x, int y, int newChest) =>
        OpenChestAction.Invoke(player, x, y, newChest);

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
    /// <param name="spinningPoint">The origin.</param>
    /// <param name="radians">The angle in radians to rotate the Vector2 by.</param>
    /// <param name="center">The center.</param>
    /// <returns>The rotated Vector2.</returns>
    public static Vector2 Rotate(this Vector2 spinningPoint, double radians, Vector2 center = default)
    {
        float num = (float)Math.Cos(radians);
        float num2 = (float)Math.Sin(radians);
        Vector2 vector = spinningPoint - center;
        Vector2 result = center;
        result.X += (vector.X * num) - (vector.Y * num2);
        result.Y += (vector.X * num2) + (vector.Y * num);
        return result;
    }

    public static TagCompound Save<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        TKey[] keys = dictionary.Keys.ToArray();
        TValue[] values = dictionary.Values.ToArray();
        var tag = new TagCompound();
        tag.Set("keys", keys);
        tag.Set("values", values);
        return tag;
    }

    /// <summary>
    ///     Used to draw float coordinates to floored coordinates to avoid blurry rendering of textures.
    /// </summary>
    /// <param name="vector">The vector to convert.</param>
    /// <returns>The floored vector.</returns>
    public static Vector2 ToNearestPixel(this Vector2 vector) => new((int)vector.X, (int)vector.Y);

    /// <summary>
    ///     Helper method for Vampire Teeth and Blah's Knives life steal.
    /// </summary>
    /// <param name="p">The player.</param>
    /// <param name="dmg">The damage to use in the life steal calculation.</param>
    /// <param name="position">The position to spawn the life steal projectile at.</param>
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
        Projectile.NewProjectile(
            p.GetSource_Accessory(new Item(ModContent.ItemType<VampireTeeth>())),
            position.X, position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, p.whoAmI, num2, num);
    }

    public static T GetRandomValue<T>(this T[] array)
    {
        var random = new Random();
        return array[random.Next(array.Length)];
    }

    private static Action<Player, int, int, int> CacheOpenChestAction()
    {
        ParameterExpression paramPlayer = Expression.Parameter(typeof(Player));
        ParameterExpression paramX = Expression.Parameter(typeof(int));
        ParameterExpression paramY = Expression.Parameter(typeof(int));
        ParameterExpression paramNewChest = Expression.Parameter(typeof(int));
        MethodInfo methodInfo = typeof(Player).GetMethod("OpenChest", BindingFlags.NonPublic | BindingFlags.Instance,
            new[] { typeof(int), typeof(int), typeof(int) })!;
        return Expression
            .Lambda<Action<Player, int, int, int>>(
                Expression.Call(
                    paramPlayer,
                    methodInfo,
                    new Expression[] { paramX, paramY, paramNewChest }),
                paramPlayer, paramX, paramY, paramNewChest).Compile();
    }
}
