using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Loamstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(95, 38, 12));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<TropicalMud>()] = true;
        Main.tileMerge[ModContent.TileType<TropicalMud>()][Type] = true;
        ItemDrop = ModContent.ItemType<LoamstoneBrick>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.TropicalMudDust>();
    }
}
