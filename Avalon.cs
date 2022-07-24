using System.Globalization;
using System.IO;
using Avalon.Common;
using Avalon.Effects;
using Avalon.Network;
using Avalon.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon;

/// <summary>
///     The main mod class.
/// </summary>
public class Avalon : Mod
{
    /// <summary>
    ///     The path for texture assets of the mod.
    /// </summary>
    public const string TextureAssetsPath = "Assets/Textures";

    /// <summary>
    ///     Gets the instance of the imkSushi's mod.
    /// </summary>
    public static readonly Mod? ImkSushisMod = ModLoader.TryGetMod("Tokens", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets the instance of the music mod for this mod.
    /// </summary>
    public static readonly Mod? MusicMod = ModLoader.TryGetMod("AvalonMusic", out Mod obtainedMod) ? obtainedMod : null;

    public static Asset<Texture2D>[] DarkMatterBackgrounds = new Asset<Texture2D>[50];
    public static Asset<Texture2D> DarkMatterBlackHole;
    public static Asset<Texture2D> DarkMatterBlackHole2;
    public static Asset<Texture2D> DarkMatterFloatingRocks;
    public static Effect DarkMatterShader;

    /// <summary>
    ///     The Dark Matter sky texture.
    /// </summary>
    public static Asset<Texture2D> DarkMatterSky;

    /// <summary>
    ///     Gets reference to the main instance of the mod.
    /// </summary>
    public static Avalon Mod { get; private set; } = ModContent.GetInstance<Avalon>();

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
        if (ModLoader.TryGetMod("Wikithis", out Mod? wikiThis) && wikiThis != null && !Main.dedServ)
        {
            wikiThis.Call(0, this, "terrariamods.fandom.com$Exxo_Avalon");
        }

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
        DarkMatterSky = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterSky");
        DarkMatterShader = ModContent
            .Request<Effect>("Avalon/Effects/DarkMatterSkyShader", AssetRequestMode.ImmediateLoad).Value;
        SkyManager.Instance["Avalon:DarkMatter"] = new DarkMatterSky();
        Filters.Scene["Avalon:DarkMatter"] = new Filter(
            new DarkMatterScreenShader(new Ref<Effect>(DarkMatterShader), "DarkMatterSky")
                .UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
        DarkMatterBlackHole = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole",
            AssetRequestMode.ImmediateLoad);
        DarkMatterFloatingRocks = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/FloatingRocks",
            AssetRequestMode.ImmediateLoad);
        DarkMatterBlackHole2 = ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterBGBlackHole2",
            AssetRequestMode.ImmediateLoad);
        for (int i = 0; i < DarkMatterBackgrounds.Length; i++)
        {
            DarkMatterBackgrounds[i] =
                ModContent.Request<Texture2D>("Avalon/Backgrounds/DarkMatter/DarkMatterCloud" + i,
                    AssetRequestMode.ImmediateLoad);
        }
    }

    public override void Unload() => Mod = null!;

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
}
