using AltLibrary;
using AltLibrary.Common.AltOres;
using Avalon.Items.Placeable.Bar;
using Avalon.Tiles.Ores;
using Terraria.ModLoader;

namespace Avalon.Compatability.AltLib
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

        public override string Texture => "Avalon/Assets/Ores/DurataniumOreIcon";
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

        public override string Texture => "Avalon/Assets/Ores/NaquadahOreIcon";
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

        public override string Texture => "Avalon/Assets/Ores/TroxiniumOreIcon";
    }
}
