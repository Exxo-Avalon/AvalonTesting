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
    internal class DurataniumAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Cobalt;

            ore = ModContent.TileType<DurataniumOre>();
            bar = ModContent.ItemType<DurataniumBar>();

            DisplayName.SetDefault("Duratanium");
            BlessingMessage.SetDefault("Your world has been blessed with Duratanium");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/DurataniumOreIcon";
    }

    internal class NaquadahAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Mythril;

            ore = ModContent.TileType<NaquadahOre>();
            bar = ModContent.ItemType<NaquadahBar>();

            DisplayName.SetDefault("Naquadah");
            BlessingMessage.SetDefault("Your world has been blessed with Naquadah");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/NaquadahOreIcon";
    }

    internal class TroxiniumAlternateOres : AltOre
    {
        public override void SetStaticDefaults()
        {
            OreType = OreType.Adamantite;

            ore = ModContent.TileType<TroxiniumOre>();
            bar = ModContent.ItemType<TroxiniumBar>();

            DisplayName.SetDefault("Troxinium");
            BlessingMessage.SetDefault("Your world has been blessed with Troxinium");
        }

        public override string Texture => "AvalonTesting/Assets/Ores/TroxiniumOreIcon";
    }
}
