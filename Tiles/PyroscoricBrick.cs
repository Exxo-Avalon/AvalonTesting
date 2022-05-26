using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class PyroscoricBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 102, 0));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.PyroscoricBrick>();
        HitSound = SoundID.Tink;
        DustType = DustID.InfernoFork;
    }
}
