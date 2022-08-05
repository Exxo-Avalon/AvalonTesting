using AltLibrary;
using AltLibrary.Common.AltBiomes;
using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.Compatability.AltLib;

internal class TropicsAlternateBiome : AltBiome
{
    public override Color NameColor => new(255, 140, 0);

    public override void SetStaticDefaults()
    {
        BiomeType = BiomeType.Jungle;

        BiomeGrass = ModContent.TileType<TropicalGrass>();
        BiomeStone = ModContent.TileType<TropicalStone>();
        BiomeOre = ModContent.TileType<XanthophyteOre>();
        BiomeMud = ModContent.TileType<Loam>();
        BiomeOreBrick = ModContent.TileType<XanthophyteOre>(); //Change to Xanthophyte Brick when its finished
        //BossBulb = ModContent.TileType<CentipedeNest>();
        GenPassName.SetDefault("Generating tropics");
        //BiomeChestItem = ModContent.ItemType<VirulentKnives>(); //Change to biome item later
        //BiomeChestTile = ModContent.TileType<LockedContagionChest>(); //change to biome chest locked later
        BiomeChestTileStyle = 0;
        BiomeMudWall = ModContent.WallType<Walls.TropicalMudWall>();
        BiomeGrassWall = ModContent.WallType<Walls.TropicalGrassWall>();
        BiomeJunglePlants = ModContent.TileType<TropicalShortGrass>();
        HiveGenerationPass = new World.Passes.WaspNest();
        TempleGenPass = new World.Passes.TuhrtlOutpost();
        BiomeJungleBushes = ModContent.TileType<TropicsBushes>();
        //BiomeShrineChestType = ModContent.TileType<PlatinumChest>();

        DisplayName.SetDefault("Tropics");
        Description.SetDefault("A Tropical forest which houses lots of wasps. A hidden outpost lives beneath the surface. [c/E11919:(Not Complete)]");

        BakeTileChild(ModContent.TileType<Loam>(), TileID.Mud, new(true, true, true));

        /*WallContext = new WallContext()
            .AddReplacement<ContagionNaturalWall1>(28, 1, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 188, 189, 190, 191, 192, 193, 194, 195, 61, 185, 212, 213, 214, 215, 3, 200, 201, 202, 203, 83)
            .AddReplacement<ContagionGrassWall>(63, 65, 66, 68, 69, 70, 81)
            .AddReplacement<ContagionNaturalWall2>(216, 217, 218, 219) //Sandstone walls
            .AddReplacement<ContagionNaturalWall2>(197, 220, 221, 222); //Hardened sand walls*/
    }

    public override string WorldIcon => "Avalon/Assets/WorldIcons/Tropics";
    public override GenPass GetHiveGenerationPass() => new World.Passes.WaspNest();
    public override string IconSmall => "Avalon/Assets/Bestiary/TropicsIcon";
}
