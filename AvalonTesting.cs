using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AvalonTesting.Common;
using AvalonTesting.Network;
using AvalonTesting.Players;
using AvalonTesting.Projectiles.Torches;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
}
