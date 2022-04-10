using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class FeroziumBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(0, 0, 250));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.FeroziumBrick>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.UltraBrightTorch;
    }
}
