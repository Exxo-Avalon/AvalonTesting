using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AvalonTesting.Common;
using AvalonTesting.Network;
using AvalonTesting.Players;
using AvalonTesting.Projectiles.Torches;
using AvalonTesting.Systems;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting;

/// <summary>
///     The main mod class.
/// </summary>
public class AvalonTesting : Mod
{
    /// <summary>
    ///     The path for texture assets of the mod.
    /// </summary>
    public const string TextureAssetsPath = "Assets/Textures";

    /// <summary>
    /// The Dark Matter sky texture.
    /// </summary>
    public static Texture2D DarkMatterSky;

    public static Asset<Texture2D>[] DarkMatterBackgrounds = new Asset<Texture2D>[50];
    public static Asset<Texture2D> DarkMatterBlackHole;
    public static Asset<Texture2D> DarkMatterBlackHole2;
    public static Asset<Texture2D> DarkMatterFloatingRocks;
    public static Effect DarkMatterShader;

    /// <summary>
    ///     Gets the instance of the imkSushi's mod.
    /// </summary>
    public static readonly Mod? ImkSushisMod = ModLoader.TryGetMod("Tokens", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets reference to the main instance of the mod.
    /// </summary>
    public static readonly AvalonTesting Mod = ModContent.GetInstance<AvalonTesting>();

    /// <summary>
    ///     Gets the instance of the music mod for this mod.
    /// </summary>
    public static readonly Mod? MusicMod = ModLoader.TryGetMod("AvalonMusic", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets or sets the transition value for fading the caesium background in and out.
    /// </summary>
    public static float CaesiumTransition { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the mouse should be checked in an interface or not.
    /// </summary>
    public bool CheckPointer { get; set; }

    /// <inheritdoc />
    public override void Load()
    {

        ModLoader.TryGetMod("Wikithis", out Mod wikithis);
        if (wikithis != null && !Main.dedServ)
            wikithis.Call(0, this, "terrariamods.fandom.com$Exxo_Avalon");

        ExxoPlayer.torches = new Dictionary<int, int>()
        {
            { ItemID.Torch, ModContent.ProjectileType<Torch>() },
            { ItemID.BlueTorch, ModContent.ProjectileType<BlueTorch>() },
            { ItemID.RedTorch, ModContent.ProjectileType<RedTorch>() },
            { ItemID.GreenTorch, ModContent.ProjectileType<GreenTorch>() },
            { ItemID.PurpleTorch, ModContent.ProjectileType<PurpleTorch>() },
            { ItemID.WhiteTorch, ModContent.ProjectileType<WhiteTorch>() },
            { ItemID.YellowTorch, ModContent.ProjectileType<YellowTorch>() },
            { ItemID.DemonTorch, ModContent.ProjectileType<DemonTorch>() },
            { ItemID.CursedTorch, ModContent.ProjectileType<CursedTorch>() },
            { ItemID.IceTorch, ModContent.ProjectileType<IceTorch>() },
            { ItemID.OrangeTorch, ModContent.ProjectileType<OrangeTorch>() },
            { ItemID.IchorTorch, ModContent.ProjectileType<IchorTorch>() },
            { ItemID.UltrabrightTorch, ModContent.ProjectileType<UltrabrightTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.JungleTorch>(), ModContent.ProjectileType<JungleTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.PathogenTorch>(), ModContent.ProjectileType<PathogenTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.SlimeTorch>(), ModContent.ProjectileType<SlimeTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.CyanTorch>(), ModContent.ProjectileType<CyanTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.LimeTorch>(), ModContent.ProjectileType<LimeTorch>() },
            { ModContent.ItemType<Items.Placeable.Light.BrownTorch>(), ModContent.ProjectileType<BrownTorch>() },
            { ItemID.BoneTorch, ModContent.ProjectileType<BoneTorch>() },
            { ItemID.RainbowTorch, ModContent.ProjectileType<RainbowTorch>() },
            { ItemID.PinkTorch, ModContent.ProjectileType<PinkTorch>() },
        };

        // ----------- Server/Client ----------- //
        while (ModHook.RegisteredHooks.TryDequeue(out ModHook? hook))
        {
            hook.ApplyHook();
        }

        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        ReplaceVanillaTextures();
        DarkMatterSky = ModContent.Request<Texture2D>("AvalonTesting/Backgrounds/DarkMatter/DarkMatterSky", AssetRequestMode.ImmediateLoad).Value;
        DarkMatterShader = ModContent.Request<Effect>("AvalonTesting/Effects/DarkMatterSkyShader", AssetRequestMode.ImmediateLoad).Value;
        SkyManager.Instance["AvalonTesting:DarkMatter"] = new Effects.DarkMatterSky();
        Filters.Scene["AvalonTesting:DarkMatter"] = new Filter(new DarkMatterScreenShader(new Ref<Effect>(DarkMatterShader), "DarkMatterSky").UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
        DarkMatterBlackHole = ModContent.Request<Texture2D>("AvalonTesting/Backgrounds/DarkMatter/DarkMatterBGBlackHole", AssetRequestMode.ImmediateLoad);
        DarkMatterFloatingRocks = ModContent.Request<Texture2D>("AvalonTesting/Backgrounds/DarkMatter/FloatingRocks", AssetRequestMode.ImmediateLoad);
        DarkMatterBlackHole2 = ModContent.Request<Texture2D>("AvalonTesting/Backgrounds/DarkMatter/DarkMatterBGBlackHole2", AssetRequestMode.ImmediateLoad);
        for (int i = 0; i < DarkMatterBackgrounds.Length; i++)
        {
            DarkMatterBackgrounds[i] = ModContent.Request<Texture2D>("AvalonTesting/Backgrounds/DarkMatter/DarkMatterCloud" + i, AssetRequestMode.ImmediateLoad);
        }
    }
    public override void Unload()
    {
        ExxoPlayer.torches = null;
        // TODO: FIGURE OUT HOW TO LOAD ORIGINAL TEXTURES UPON UNLOADING AVALON.
        //if (Main.netMode != NetmodeID.Server)
        //{
        //    Main.logoTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo");
        //    Main.logo2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo2");
        //}
    }
    /// <inheritdoc />
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        byte msgIndex = reader.ReadByte();
        if (msgIndex < NetworkManager.RegisteredHandlers.Count)
        {
            NetworkManager.RegisteredHandlers[msgIndex].Handle(reader, whoAmI);
        }
        else
        {
            Logger.Error(
                $"PacketHandler with message index {msgIndex.ToString(CultureInfo.InvariantCulture)} does not exist");
        }
    }
    public override void AddRecipes()
    {
        if (ImkSushisMod != null)
        {
            SushiRecipes.CreateRecipes(ImkSushisMod);
        }
    }
    private void ReplaceVanillaTextures()
    {
        // Items
        TextureAssets.Item[ItemID.HallowedKey] = Assets.Request<Texture2D>("Sprites/HallowedKey");
        TextureAssets.Item[ItemID.MagicDagger] = Assets.Request<Texture2D>("Sprites/MagicDagger");
        TextureAssets.Item[ItemID.PaladinBanner] = Assets.Request<Texture2D>("Sprites/PaladinBanner");
        TextureAssets.Item[ItemID.PossessedArmorBanner] =
            Assets.Request<Texture2D>("Sprites/PossessedArmorBanner");
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

    // why this method is in here? good question!
    internal static Color ReturnHardmodeColor(int i)
	{
        if (i == TileID.Cobalt)
            return new Color(26, 105, 161);
        else if (i == TileID.Palladium)
            return new Color(235, 87, 47);
        else if (i == ModContent.TileType<DurataniumOre>())
            return new Color(137, 81, 89);
        else if (i == TileID.Mythril)
            return new Color(93, 147, 88);
        else if (i == TileID.Orichalcum)
            return new Color(163, 22, 158);
        else if (i == ModContent.TileType<NaquadahOre>())
            return new Color(0, 38, 255);
        else if (i == TileID.Adamantite)
            return new Color(221, 85, 152);
        else if (i == TileID.Titanium)
            return new Color(185, 194, 215);
        else if (i == ModContent.TileType<TroxiniumOre>())
            return new Color(193, 218, 72);
        return new Color(50, 255, 130);
	}
}
