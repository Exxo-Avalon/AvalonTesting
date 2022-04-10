using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class RhodiumBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Pink);
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.RhodiumBrick>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.t_LivingWood;
    }
}
