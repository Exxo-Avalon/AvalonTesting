using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class OrangeBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(163, 66, 14));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
        ItemDrop = Mod.Find<ModItem>("OrangeBrick").Type;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Coralstone;
    }
}
