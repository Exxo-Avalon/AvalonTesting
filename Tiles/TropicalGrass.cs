using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class TropicalGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(115, 159, 57));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<LoamBlock>();
        Main.tileMerge[Type][ModContent.TileType<Loam>()] = true;
        Main.tileMerge[ModContent.TileType<Loam>()][Type] = true;
        Main.tileMerge[TileID.Grass][Type] = true;
        Main.tileMerge[Type][TileID.Grass] = true;
        Main.tileMerge[TileID.JungleGrass][Type] = true;
        Main.tileMerge[Type][TileID.JungleGrass] = true;
        TileID.Sets.Conversion.Grass[Type] = true;
        //TileID.Sets.CanBeDugByShovel[Type] = true;
        //TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = false;
        //TileID.Sets.GrassSpecial[Type] = true;
        //TileID.Sets.ChecksForMerge[Type] = true;
        //TileID.Sets.SpreadOverground[Type] = true;
        //TileID.Sets.SpreadUnderground[Type] = true;
        //TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<Loam>();
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = (ushort)ModContent.TileType<Loam>();
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
