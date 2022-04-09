using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ancient;

public class AncientGoldBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(204, 181, 72));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1800;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientGoldBrick>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Gold;
    }
}
