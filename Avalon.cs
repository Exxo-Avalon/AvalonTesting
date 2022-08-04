using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AltLibrary.Common.Systems;
using Avalon.Assets;
using Avalon.Common;
using Avalon.Effects;
using Avalon.Hooks;
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

    private readonly List<IReplaceAssets> assetReplacers = new();

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

        AvalonReflection.Init();
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        if (ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement)
        {
            ReplaceVanillaTextures();
        }
        Asset<Effect> shader =
            ModContent.Request<Effect>("Avalon/Effects/DarkMatterSkyShader", AssetRequestMode.ImmediateLoad);
        SkyManager.Instance["Avalon:DarkMatter"] = new DarkMatterSky();
        Filters.Scene["Avalon:DarkMatter"] = new Filter(
            new DarkMatterScreenShader(new Ref<Effect>(shader.Value), "DarkMatterSky")
                .UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
    }

    public override void Unload()
    {
        Mod = null!;
        AvalonReflection.Unload();
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        foreach (IReplaceAssets assetReplacer in assetReplacers)
        {
            assetReplacer.RestoreAssets();
        }
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
    public override void PostSetupContent()
    {
        AvalonCall.Support();
    }
    private void ReplaceVanillaTextures()
    {
        var itemReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Item);
        assetReplacers.Add(itemReplacer);
        itemReplacer.ReplaceAsset(ItemID.HallowedKey, Assets.Request<Texture2D>("Sprites/HallowedKey"));
        itemReplacer.ReplaceAsset(ItemID.MagicDagger, Assets.Request<Texture2D>("Sprites/MagicDagger"));
        itemReplacer.ReplaceAsset(ItemID.PaladinBanner, Assets.Request<Texture2D>("Sprites/PaladinBanner"));
        itemReplacer.ReplaceAsset(ItemID.PossessedArmorBanner,
            Assets.Request<Texture2D>("Sprites/PossessedArmorBanner"));
        itemReplacer.ReplaceAsset(ItemID.BoneLeeBanner, Assets.Request<Texture2D>("Sprites/BoneLeeBanner"));
        itemReplacer.ReplaceAsset(ItemID.AngryTrapperBanner, Assets.Request<Texture2D>("Sprites/AngryTrapperBanner"));
        itemReplacer.ReplaceAsset(ItemID.Deathweed, Assets.Request<Texture2D>("Sprites/Deathweed"));
        itemReplacer.ReplaceAsset(ItemID.WaterleafSeeds, Assets.Request<Texture2D>("Sprites/WaterleafSeeds"));
        itemReplacer.ReplaceAsset(ItemID.ShroomiteDiggingClaw,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/ShroomiteDiggingClaws"));

        var tileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Tile);
        assetReplacers.Add(tileReplacer);
        tileReplacer.ReplaceAsset(TileID.CopperCoinPile, Assets.Request<Texture2D>("Sprites/CopperCoin"));
        tileReplacer.ReplaceAsset(TileID.SilverCoinPile, Assets.Request<Texture2D>("Sprites/SilverCoin"));
        tileReplacer.ReplaceAsset(TileID.GoldCoinPile, Assets.Request<Texture2D>("Sprites/GoldCoin"));
        tileReplacer.ReplaceAsset(TileID.PlatinumCoinPile, Assets.Request<Texture2D>("Sprites/PlatinumCoin"));
        tileReplacer.ReplaceAsset(TileID.Banners, Assets.Request<Texture2D>("Sprites/VanillaBanners"));
        tileReplacer.ReplaceAsset(TileID.Containers, Assets.Request<Texture2D>("Sprites/VanillaChests"));

        var projectileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Projectile);
        assetReplacers.Add(projectileReplacer);
        projectileReplacer.ReplaceAsset(ProjectileID.MagicDagger, Assets.Request<Texture2D>("Sprites/MagicDagger"));
    }
}
