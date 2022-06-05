using AltLibrary;
using AltLibrary.Common.AltOres;
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
using AvalonTesting.Items.Placeable.Bar;

namespace AvalonTesting
{
    internal class BronzeAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Copper;

            ore = ModContent.TileType<BronzeOre>();
            bar = ModContent.ItemType<BronzeBar>();

            DisplayName.SetDefault("Bronze");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/BronzeOreIcon";
    }

    internal class NickelAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Iron;

            ore = ModContent.TileType<NickelOre>();
            bar = ModContent.ItemType<NickelBar>();

            DisplayName.SetDefault("Nickel");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/NickelOreIcon";
    }

    internal class ZincAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Silver;

            ore = ModContent.TileType<ZincOre>();
            bar = ModContent.ItemType<ZincBar>();

            DisplayName.SetDefault("Zinc");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/ZincOreIcon";
    }

    internal class BismuthAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Gold;

            ore = ModContent.TileType<BismuthOre>();
            bar = ModContent.ItemType<BismuthBar>();

            DisplayName.SetDefault("Bismuth");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/BismuthOreIcon";
    }
}
