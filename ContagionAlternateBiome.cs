using AltLibrary;
using AltLibrary.Common.AltBiomes;
using AltLibrary.Common.Systems;
using AltLibrary.Common.Hooks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using AvalonTesting.Items.Placeable.Seed;
using AvalonTesting.Items.Weapons.Melee;
using AvalonTesting.NPCs;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using AvalonTesting.Walls;

namespace AvalonTesting
{
    internal class ContagionAlternateBiome : AltBiome
    {
        public override Color NameColor => new(0, 255, 128);

        public override void SetStaticDefaults()
        {
            BiomeType = BiomeType.Evil;

            BiomeGrass = ModContent.TileType<Ickgrass>();
            BiomeStone = ModContent.TileType<Chunkstone>();
            BiomeSand = ModContent.TileType<Snotsand>();
            BiomeIce = ModContent.TileType<YellowIce>();
            BiomeSandstone = ModContent.TileType<Snotsandstone>();
            BiomeHardenedSand = ModContent.TileType<HardenedSnotsand>();
            BiomeOre = ModContent.TileType<BacciliteOre>();
            BiomeOreBrick = ModContent.TileType<ChunkstoneBrick>(); //Change to Baccilite Brick when its finished
            AltarTile = ModContent.TileType<IckyAltar>();

            BiomeOreItem = ModContent.ItemType<Items.Placeable.Tile.BacciliteOre>();
            SeedType = ModContent.ItemType<IckgrassSeeds>();

            BiomeChestItem = ModContent.ItemType<VirulentKnives>();
            BiomeChestTile = ModContent.TileType<LockedContagionChest>();
            BiomeChestTileStyle = 0;

            /*MimicKeyType = ItemID.NightKey;
            MimicType = ModContent.NPCType<ContagionMimic>();
            BloodBunny = ModContent.NPCType<InfectedBunny>();
            BloodGoldfish = ModContent.NPCType<InfectedGoldfish>();
            BloodPenguin = ModContent.NPCType<InfectedPenguin>();*/

            DisplayName.SetDefault("Contagion");
            Description.SetDefault("An infested green biome that consists of a disgusting landscape filled with vile giant bacteria");

            /*WallContext = new WallContext()
                .AddReplacement<ContagionNaturalWall1>(28, 1, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 188, 189, 190, 191, 192, 193, 194, 195, 61, 185, 212, 213, 214, 215, 3, 200, 201, 202, 203, 83)
                .AddReplacement<ContagionGrassWall>(63, 65, 66, 68, 69, 70, 81)
                .AddReplacement<ContagionNaturalWall2>(216, 217, 218, 219) //Sandstone walls
                .AddReplacement<ContagionNaturalWall2>(197, 220, 221, 222); //Hardened sand walls*/
        }

        public override List<int> SpreadingTiles => new() { ModContent.TileType<Ickgrass>(), ModContent.TileType<Chunkstone>(), ModContent.TileType<Snotsand>(), ModContent.TileType<GreenIce>(), ModContent.TileType<Snotsandstone>(), ModContent.TileType<HardenedSnotsand>() };

        public override string WorldIcon => "AvalonTesting/Assets/WorldIcons/Contagion";

        public override string IconSmall => "AvalonTesting/Sprites/Bestiary/ContagionIcon";

        public override string IconLarge => "AvalonTesting/Assets/Textures/UI/ContagionPreview";

        public override string OuterTexture => "AvalonTesting/Assets/Loading/OuterContagion";
        public override Color OuterColor => new(175, 148, 199);
    }
}
