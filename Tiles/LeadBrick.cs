using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class LeadBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(62, 82, 114));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.LeadBrick>();
        HitSound = SoundID.Tink;
        DustType = DustID.Lead;
    }
}
