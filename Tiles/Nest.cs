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
        AvalonTesting.MergeWith(Type, ModContent.TileType<TropicalMud>());
        AvalonTesting.MergeWith(Type, ModContent.TileType<TropicalGrass>());
    }

    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        AvalonTesting.MergeWithFrame(i, j, Type, ModContent.TileType<TropicalMud>(), false, false, false, false, resetFrame);
        return false;
    }
}
