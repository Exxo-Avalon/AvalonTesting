using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Avalon.Items.Accessories;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon;

public static class ClassExtensions
{
    private static readonly Action<Player, int, int, int> OpenChestAction = CacheOpenChestAction();

    public static void Active(this Tile t, bool a) => t.HasTile = a;

    public static ExxoPlayer Avalon(this Player p) => p.GetModPlayer<ExxoPlayer>();

    public static ExxoBiomePlayer AvalonBiome(this Player p) => p.GetModPlayer<ExxoBiomePlayer>();

    public static bool Exists(this Item item) => item.type > ItemID.None && item.stack > 0;
    public static bool IntersectsConeSlowMoreAccurate(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
    {
        Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
        if (DoesFitInCone(targetRect.ClosestPointInRect(point), coneCenter, coneLength, coneRotation, maximumAngle))
        {
            return true;
        }
        if (DoesFitInCone(targetRect.TopLeft(), coneCenter, coneLength, coneRotation, maximumAngle))
        {
            return true;
        }
        if (DoesFitInCone(targetRect.TopRight(), coneCenter, coneLength, coneRotation, maximumAngle))
        {
            return true;
        }
        if (DoesFitInCone(targetRect.BottomLeft(), coneCenter, coneLength, coneRotation, maximumAngle))
        {
            return true;
        }
        if (DoesFitInCone(targetRect.BottomRight(), coneCenter, coneLength, coneRotation, maximumAngle))
        {
            return true;
        }
        return false;
    }

    public static bool DoesFitInCone(Vector2 point, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
    {
        Vector2 spinningpoint = point - coneCenter;
        float num = spinningpoint.RotatedBy(0f - coneRotation).ToRotation();
        if (num < 0f - maximumAngle || num > maximumAngle)
        {
            return false;
        }
        return spinningpoint.Length() < coneLength;
    }
    public static bool IntersectsCone(this Rectangle targetRect, Vector2 coneCenter, float coneLength, float coneRotation, float maximumAngle)
    {
        Vector2 point = coneCenter + coneRotation.ToRotationVector2() * coneLength;
        Vector2 spinningpoint = targetRect.ClosestPointInRect(point) - coneCenter;
        float num = spinningpoint.RotatedBy(0f - coneRotation).ToRotation();
        if (num < 0f - maximumAngle || num > maximumAngle)
        {
            return false;
        }
        return spinningpoint.Length() < coneLength;
    }
    public static bool CanSpawnFishingRift(Vector2 pos, int type, int range)
    {
        for (int i = 0; i < 200; i++)
        {
            if (Main.npc[i].type == type && Main.npc[i].active && Vector2.Distance(pos, Main.npc[i].position) < range)
            {
                return false;
            }
        }
        return true;
    }
    public static Color MultiplyByColor(this Color c, Color c2)
    {
        float r1 = (c.R + (c2.R - c.R) / 2f);
        Main.NewText(r1);
        float g1 = (c.G + (c2.G - c.G) / 2f);
        float b1 = (c.B + (c2.B - c.B) / 2f);
        return new Color(r1, g1, b1);
    }
    public static Asset<T> VanillaLoad<T>(this Asset<T> asset) where T : class
    {
        try
        {
            if (asset.State == AssetState.NotLoaded)
            {
                Main.Assets.Request<Texture2D>(asset.Name, AssetRequestMode.ImmediateLoad);
            }
        }
        catch (AssetLoadException e)
        {
        }

        return asset;
    }
    public static Vector2 Clamp(this Vector2 input, Vector2 clampTo, float distance)
    {
        if (input.X > clampTo.X)
        {
            input.X = MathHelper.Clamp(input.X, clampTo.X - distance, clampTo.X);
        }
        else
        {
            input.X = MathHelper.Clamp(input.X, clampTo.X, clampTo.X + distance);
        }
        if (input.Y > clampTo.Y)
        {
            input.Y = MathHelper.Clamp(input.Y, clampTo.Y - distance, clampTo.Y);
        }
        else
        {
            input.Y = MathHelper.Clamp(input.Y, clampTo.Y, clampTo.Y + distance);
        }
        return input;
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
    ///     Used to draw float coordinates to rounded coordinates to avoid blurry rendering of textures.
    /// </summary>
    /// <param name="vector">The vector to convert.</param>
    /// <returns>The rounded vector.</returns>
    public static Vector2 ToNearestPixel(this Vector2 vector) => new((int)(vector.X + 0.5f), (int)(vector.Y + 0.5f));

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
