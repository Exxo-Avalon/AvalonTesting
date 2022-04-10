using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class TuhrtlBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(78, 70, 67));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.TuhrtlBrick>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 210;
        DustType = DustID.Silt;
        TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
        TileID.Sets.ForcedDirtMerging[Type] = true;
        TileID.Sets.JungleSpecial[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
