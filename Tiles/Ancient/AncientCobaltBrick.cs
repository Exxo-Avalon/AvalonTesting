using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient;

public class AncientCobaltBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(37, 118, 171));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1850;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Ancient.AncientCobaltBrick>();
        HitSound = SoundID.Tink;
        DustType = DustID.Cobalt;
    }
}
