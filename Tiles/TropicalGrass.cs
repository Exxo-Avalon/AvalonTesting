using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class TropicalGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(56, 215, 29));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<TropicalMudBlock>();
        TileID.Sets.Conversion.Grass[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
        TileID.Sets.GrassSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.SpreadOverground[Type] = true;
        TileID.Sets.SpreadUnderground[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        // do drop
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = (ushort)ModContent.TileType<TropicalMud>();
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
