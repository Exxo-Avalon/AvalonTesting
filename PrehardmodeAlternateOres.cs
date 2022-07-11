using System.Collections.Generic;
using AltLibrary;
using AltLibrary.Common;
using AltLibrary.Common.AltOres;
using AvalonTesting.Items.Placeable.Bar;
using AvalonTesting.Systems;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting;

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

internal class RhodiumAlternateOres : AltOre
{
    public override void SetStaticDefaults()
    {
        OreType = OreType.Gold;
        ore = ModContent.TileType<RhodiumOre>();
        bar = ModContent.ItemType<RhodiumBar>();
        DisplayName.SetDefault("Rhodium");
        Selectable = false;
    }
    public override string Texture => "AvalonTesting/Assets/Ores/RhodiumOreIcon";
    public override void OnInitialize() => ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = (ExxoWorldGen.RhodiumVariant)Main.rand.Next(3);

    public override bool OnClick()
    {
        ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = ExxoWorldGen.RhodiumVariant.Rhodium;
        return true;
    }

    public override void CustomSelection(List<AltOre> list)
    {
        int index = list.FindIndex(x => x.FullName == "Terraria/Platinum");
        if (index != -1)
        {
            list.Insert(index + 1, this);
            list.Insert(index + 2, Mod.Find<AltOre>("OsmiumAlternateOres"));
            list.Insert(index + 3, Mod.Find<AltOre>("IridiumAlternateOres"));
        }
    }

    public override void AddOreOnScreenIcon(List<ALOreDrawingStruct> list)
    {
        int index = list.FindIndex(x => x.UniqueID == "Terraria/Gold");
        if (index != -1)
        {
            list.Insert(index + 1, new ALOreDrawingStruct(this, false,
                (value) => ModContent.GetInstance<ExxoWorldGen>().RhodiumOre switch
                    {
                        ExxoWorldGen.RhodiumVariant.Rhodium => ModContent.Request<Texture2D>("AvalonTesting/Assets/Ores/RhodiumOreIcon"),
                        ExxoWorldGen.RhodiumVariant.Osmium => ModContent.Request<Texture2D>("AvalonTesting/Assets/Ores/OsmiumOreIcon"),
                        ExxoWorldGen.RhodiumVariant.Iridium => ModContent.Request<Texture2D>("AvalonTesting/Assets/Ores/IridiumOreIcon"),
                        _ => value,
                    }, () => new Rectangle(0, 0, 30, 30),
                () => ModContent.GetInstance<ExxoWorldGen>().RhodiumOre switch
                    {
                        ExxoWorldGen.RhodiumVariant.Rhodium => "Rhodium",
                        ExxoWorldGen.RhodiumVariant.Osmium => "Osmium",
                        ExxoWorldGen.RhodiumVariant.Iridium => "Iridium",
                        _ => throw new System.NotImplementedException(),
                    },
                (_) => "AvalonTesting"));
        }
    }
}

internal class OsmiumAlternateOres : AltOre
{
    public override void SetStaticDefaults()
    {
        OreType = OreType.Gold;
        ore = ModContent.TileType<OsmiumOre>();
        bar = ModContent.ItemType<OsmiumBar>();
        DisplayName.SetDefault("Osmium");
        Selectable = false;
    }
    public override string Texture => "AvalonTesting/Assets/Ores/OsmiumOreIcon";
    public override bool OnClick()
    {
        ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = ExxoWorldGen.RhodiumVariant.Osmium;
        return true;
    }

    public override void CustomSelection(List<AltOre> list)
    {
        // vs, you suck, this method actually cancels default behavior.
    }
}

internal class IridiumAlternateOres : AltOre
{
    public override void SetStaticDefaults()
    {
        OreType = OreType.Gold;
        ore = ModContent.TileType<IridiumOre>();
        bar = ModContent.ItemType<IridiumBar>();
        DisplayName.SetDefault("Iridium");
        Selectable = false;
    }
    public override string Texture => "AvalonTesting/Assets/Ores/IridiumOreIcon";
    public override bool OnClick()
    {
        ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = ExxoWorldGen.RhodiumVariant.Iridium;
        return true;
    }

    public override void CustomSelection(List<AltOre> list)
    {
        // vs, you suck, this method actually cancels default behavior.
    }
}
