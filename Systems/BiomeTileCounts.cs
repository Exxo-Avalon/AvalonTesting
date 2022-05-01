using System;
using AvalonTesting.Players;
using AvalonTesting.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AvalonTesting.Systems;

public class BiomeTileCounts : ModSystem
{
    // Remove or not???
    public const int DarkMatterTilesHardLimit = 250000;
    public int WorldDarkMatterTiles;
    public int ContagionTiles { get; private set; }
    public int TropicsTiles { get; private set; }
    public int HellCastleTiles { get; private set; }
    public int DarkTiles { get; private set; }
    public int CaesiumTiles { get; private set; }
    public int SkyFortressTiles { get; private set; }
    public int CrystalTiles { get; private set; }

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        Main.SceneMetrics.JungleTileCount += tileCounts[ModContent.TileType<GreenIce>()];
        Main.SceneMetrics.SandTileCount += tileCounts[ModContent.TileType<Snotsand>()];
        ContagionTiles = tileCounts[ModContent.TileType<Chunkstone>()] +
                         tileCounts[ModContent.TileType<HardenedSnotsand>()] +
                         tileCounts[ModContent.TileType<Snotsandstone>()] +
                         tileCounts[ModContent.TileType<Ickgrass>()] +
                         tileCounts[ModContent.TileType<Snotsand>()] +
                         tileCounts[ModContent.TileType<YellowIce>()];

        TropicsTiles = tileCounts[ModContent.TileType<TropicalStone>()] +
                       tileCounts[ModContent.TileType<TuhrtlBrick>()] +
                       tileCounts[ModContent.TileType<TropicalMud>()] +
                       tileCounts[ModContent.TileType<TropicalGrass>()];
        HellCastleTiles = tileCounts[ModContent.TileType<ImperviousBrick>()];
        DarkTiles = tileCounts[ModContent.TileType<DarkMatter>()] +
                    tileCounts[ModContent.TileType<DarkMatterSand>()] +
                    tileCounts[ModContent.TileType<BlackIce>()] +
                    tileCounts[ModContent.TileType<DarkMatterSoil>()] +
                    tileCounts[ModContent.TileType<HardenedDarkSand>()] +
                    tileCounts[ModContent.TileType<Darksandstone>()] +
                    tileCounts[ModContent.TileType<DarkMatterGrass>()];

        CaesiumTiles = tileCounts[ModContent.TileType<BlastedStone>()];
        SkyFortressTiles = tileCounts[ModContent.TileType<SkyBrick>()];
        CrystalTiles = tileCounts[ModContent.TileType<CrystalStone>()];

        Main.LocalPlayer.GetModPlayer<ExxoBiomePlayer>().UpdateZones(this);
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["WorldDarkMatterTiles"] = WorldDarkMatterTiles;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("WorldDarkMatterTiles"))
        {
            WorldDarkMatterTiles = tag.Get<int>("WorldDarkMatterTiles");
        }
    }
}
