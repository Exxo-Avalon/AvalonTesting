using AvalonTesting.Hooks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;

public partial class AvalonTesting : Mod
{
    /// <summary>
    /// Reference to the main instance of the mod
    /// </summary>
    public static AvalonTesting Mod { get; private set; }
    public Mod MusicMod { get; private set; }

    public AvalonTesting()
    {
        Mod = this;
    }

    public override void Load()
    {
        // ----------- Server/Client ----------- //
        HooksManager.ApplyHooks();

        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        ModLoader.TryGetMod("AvalonMusic", out Mod musicMod);
        MusicMod = musicMod;
        ReplaceVanillaTextures();
    }

    private void ReplaceVanillaTextures()
    {
        // Items
        TextureAssets.Item[ItemID.HallowedKey] = Assets.Request<Texture2D>("Sprites/HallowedKey");
        TextureAssets.Item[ItemID.MagicDagger] = Assets.Request<Texture2D>("Sprites/MagicDagger");
        TextureAssets.Item[ItemID.PaladinBanner] = Assets.Request<Texture2D>("Sprites/PaladinBanner");
        TextureAssets.Item[ItemID.PossessedArmorBanner] = Assets.Request<Texture2D>("Sprites/PossessedArmorBanner");
        TextureAssets.Item[ItemID.BoneLeeBanner] = Assets.Request<Texture2D>("Sprites/BoneLeeBanner");
        TextureAssets.Item[ItemID.AngryTrapperBanner] = Assets.Request<Texture2D>("Sprites/AngryTrapperBanner");
        TextureAssets.Item[ItemID.Deathweed] = Assets.Request<Texture2D>("Sprites/Deathweed");
        TextureAssets.Item[ItemID.WaterleafSeeds] = Assets.Request<Texture2D>("Sprites/WaterleafSeeds");
        // Tiles
        TextureAssets.Tile[TileID.CopperCoinPile] = Assets.Request<Texture2D>("Sprites/CopperCoin");
        TextureAssets.Tile[TileID.SilverCoinPile] = Assets.Request<Texture2D>("Sprites/SilverCoin");
        TextureAssets.Tile[TileID.GoldCoinPile] = Assets.Request<Texture2D>("Sprites/GoldCoin");
        TextureAssets.Tile[TileID.PlatinumCoinPile] = Assets.Request<Texture2D>("Sprites/PlatinumCoin");
        TextureAssets.Tile[TileID.Banners] = Assets.Request<Texture2D>("Sprites/VanillaBanners");
        TextureAssets.Tile[TileID.Containers] = Assets.Request<Texture2D>("Sprites/VanillaChests");
        // Projectiles
        TextureAssets.Projectile[ProjectileID.MagicDagger] = Assets.Request<Texture2D>("Sprites/MagicDagger");
    }

    public enum Similarity
    {
        None,
        Same,
        Merge
    }

    public static Similarity GetSimilarity(Tile check, int myType, int mergeType)
    {
        if (check.HasTile != true)
        {
            return Similarity.None;
        }

        if (check.TileType == myType || Main.tileMerge[myType][check.TileType])
        {
            return Similarity.Same;
        }

        if (check.TileType == mergeType)
        {
            return Similarity.Merge;
        }

        return Similarity.None;
    }

    public static void StopRain()
    {
        Main.rainTime = 0;
        Main.raining = false;
        Main.maxRaining = 0f;
    }

    public static void StartRain()
    {
        const int num = 86400;
        const int num2 = num / 24;
        Main.rainTime = Main.rand.Next(num2 * 8, num);
        if (Main.rand.Next(3) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2);
        }

        if (Main.rand.Next(4) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.Next(5) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 2);
        }

        if (Main.rand.Next(6) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 3);
        }

        if (Main.rand.Next(7) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 4);
        }

        if (Main.rand.Next(8) == 0)
        {
            Main.rainTime += Main.rand.Next(0, num2 * 5);
        }

        float num3 = 1f;
        if (Main.rand.Next(2) == 0)
        {
            num3 += 0.05f;
        }

        if (Main.rand.Next(3) == 0)
        {
            num3 += 0.1f;
        }

        if (Main.rand.Next(4) == 0)
        {
            num3 += 0.15f;
        }

        if (Main.rand.Next(5) == 0)
        {
            num3 += 0.2f;
        }

        Main.rainTime = (int)(Main.rainTime * num3);
        ChangeRain();
        Main.raining = true;
    }

    public static void ChangeRain()
    {
        if (Main.cloudBGActive >= 1f || Main.numClouds > 150.0)
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(20, 90) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(40, 90) * 0.01f;
        }
        else if (Main.numClouds > 100.0)
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(10, 70) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(20, 60) * 0.01f;
        }
        else
        {
            if (Main.rand.Next(3) == 0)
            {
                Main.maxRaining = Main.rand.Next(5, 40) * 0.01f;
                return;
            }

            Main.maxRaining = Main.rand.Next(5, 30) * 0.01f;
        }
    }
}
