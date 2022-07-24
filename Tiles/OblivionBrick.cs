using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class OblivionBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.DarkViolet);
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OblivionBrick>();
        HitSound = SoundID.Tink;
        DustType = DustID.Adamantite;
    }
}
