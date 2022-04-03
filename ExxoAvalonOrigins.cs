using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ExxoAvalonOrigins;

public class ExxoAvalonOrigins : Mod
{
#if DEBUG
    public const bool DevMode = true;
#else
    public const bool DevMode = false;
#endif
    public new readonly Version Version = new(1, 0, 0, 0, DevMode);

    // Reference to the main instance of the mod
    public static ExxoAvalonOrigins Mod { get; private set; }
    public Mod MusicMod;
    public ExxoAvalonOrigins()
    {
        Mod = this;
    }
    public override void Load()
    {
        if (Main.netMode != NetmodeID.Server)
        {
            if (ModLoader.GetMod("AvalonMusic") != null)
            {
                MusicMod = ModLoader.GetMod("AvalonMusic");
            }
            TextureAssets.Logo = ModContent.Request<Texture2D>("Sprites/EAOLogo");
            TextureAssets.Logo2 = ModContent.Request<Texture2D>("Sprites/EAOLogo");
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                TextureAssets.Logo = ModContent.Request<Texture2D>("Sprites/EAOLogoAprilFools");
                TextureAssets.Logo2 = ModContent.Request<Texture2D>("Sprites/EAOLogoAprilFools");
            }
            TextureAssets.Item[ItemID.HallowedKey] = ModContent.Request<Texture2D>("Sprites/HallowedKey");
            TextureAssets.Item[ItemID.MagicDagger] = ModContent.Request<Texture2D>("Sprites/MagicDagger");
            TextureAssets.Tile[TileID.CopperCoinPile] = ModContent.Request<Texture2D>("Sprites/CopperCoin");
            TextureAssets.Tile[TileID.SilverCoinPile] = ModContent.Request<Texture2D>("Sprites/SilverCoin");
            TextureAssets.Tile[TileID.GoldCoinPile] = ModContent.Request<Texture2D>("Sprites/GoldCoin");
            TextureAssets.Tile[TileID.PlatinumCoinPile] = ModContent.Request<Texture2D>("Sprites/PlatinumCoin");
            TextureAssets.Item[ItemID.PaladinBanner] = ModContent.Request<Texture2D>("Sprites/PaladinBanner");
            TextureAssets.Item[ItemID.PossessedArmorBanner] = ModContent.Request<Texture2D>("Sprites/PossessedArmorBanner");
            TextureAssets.Item[ItemID.BoneLeeBanner] = ModContent.Request<Texture2D>("Sprites/BoneLeeBanner");
            TextureAssets.Item[ItemID.AngryTrapperBanner] = ModContent.Request<Texture2D>("Sprites/AngryTrapperBanner");
            TextureAssets.Item[ItemID.Deathweed] = ModContent.Request<Texture2D>("Sprites/Deathweed");
            TextureAssets.Item[ItemID.WaterleafSeeds] = ModContent.Request<Texture2D>("Sprites/WaterleafSeeds");
            TextureAssets.Projectile[ProjectileID.MagicDagger] = ModContent.Request<Texture2D>("Sprites/MagicDagger");
            TextureAssets.Tile[TileID.Banners] = ModContent.Request<Texture2D>("Sprites/VanillaBanners");
            TextureAssets.Tile[TileID.Containers] = ModContent.Request<Texture2D>("Sprites/VanillaChests");
        }
    }
}
