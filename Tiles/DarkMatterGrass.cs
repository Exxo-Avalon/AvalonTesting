using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatterGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(120, 15, 170));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Grass[Type] = true;
        TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;
        TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
        TileID.Sets.SpreadOverground[Type] = true;
        TileID.Sets.SpreadUnderground[Type] = true;
        TileID.Sets.Grass[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<DarkMatterSoilBlock>();
        DustType = ModContent.DustType<DarkMatterDust>();
    }

    public override int SaplingGrowthType(ref int style)
    {
        style = 0;
        return ModContent.TileType<DarkMatterSapling>();
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail)
        {
            AvalonTestingWorld.WorldDarkMatterTiles--;
        }

        base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
    }
}
