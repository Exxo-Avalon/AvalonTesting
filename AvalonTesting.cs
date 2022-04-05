using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AvalonTesting;

public partial class AvalonTesting : Mod
{
#if DEBUG
    public const bool DevMode = true;
#else
    public const bool DevMode = false;
#endif
    public new readonly Version Version = new(1, 0, 0, 0, DevMode);

    // Reference to the main instance of the mod
    public static AvalonTesting Mod { get; private set; }
    public Mod MusicMod;

    public AvalonTesting()
    {
        Mod = this;
    }

    public override void Load()
    {
        if (Main.netMode != NetmodeID.Server)
        {
            ModLoader.TryGetMod("AvalonMusic", out MusicMod);
            TextureAssets.Logo = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogo");
            TextureAssets.Logo2 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogo");
            TextureAssets.Logo3 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogo");
            TextureAssets.Logo4 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogo");
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                TextureAssets.Logo = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogoAprilFools");
                TextureAssets.Logo2 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogoAprilFools");
                TextureAssets.Logo3 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogoAprilFools");
                TextureAssets.Logo4 = ModContent.Request<Texture2D>("AvalonTesting/Sprites/EAOLogoAprilFools");
            }

            Players.ExxoPlayer.spectrumArmorTextures = new ReLogic.Content.Asset<Texture2D>[]
            {
                ModContent.Request<Texture2D>("AvalonTesting/Items/Armor/SpectrumHelmet_Glow_Head"),
                ModContent.Request<Texture2D>("AvalonTesting/Items/Armor/SpectrumBreastplate_Body_Glow"),
                ModContent.Request<Texture2D>("AvalonTesting/Items/Armor/SpectrumGreaves_Legs_Glow")
            };
            TextureAssets.Item[ItemID.HallowedKey] = ModContent.Request<Texture2D>("AvalonTesting/Sprites/HallowedKey");
            TextureAssets.Item[ItemID.MagicDagger] = ModContent.Request<Texture2D>("AvalonTesting/Sprites/MagicDagger");
            TextureAssets.Tile[TileID.CopperCoinPile] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/CopperCoin");
            TextureAssets.Tile[TileID.SilverCoinPile] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/SilverCoin");
            TextureAssets.Tile[TileID.GoldCoinPile] = ModContent.Request<Texture2D>("AvalonTesting/Sprites/GoldCoin");
            TextureAssets.Tile[TileID.PlatinumCoinPile] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/PlatinumCoin");
            TextureAssets.Item[ItemID.PaladinBanner] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/PaladinBanner");
            TextureAssets.Item[ItemID.PossessedArmorBanner] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/PossessedArmorBanner");
            TextureAssets.Item[ItemID.BoneLeeBanner] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/BoneLeeBanner");
            TextureAssets.Item[ItemID.AngryTrapperBanner] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/AngryTrapperBanner");
            TextureAssets.Item[ItemID.Deathweed] = ModContent.Request<Texture2D>("AvalonTesting/Sprites/Deathweed");
            TextureAssets.Item[ItemID.WaterleafSeeds] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/WaterleafSeeds");
            TextureAssets.Projectile[ProjectileID.MagicDagger] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/MagicDagger");
            TextureAssets.Tile[TileID.Banners] = ModContent.Request<Texture2D>("AvalonTesting/Sprites/VanillaBanners");
            TextureAssets.Tile[TileID.Containers] =
                ModContent.Request<Texture2D>("AvalonTesting/Sprites/VanillaChests");
        }
    }
}
