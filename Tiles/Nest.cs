using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Nest : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(198, 175, 132));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<NestBlock>();
        DustType = DustID.MarblePot;
        TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true;
        TileID.Sets.JungleSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = true;
    }
}
