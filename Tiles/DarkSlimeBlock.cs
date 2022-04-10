using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkSlimeBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(63, 0, 63));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.DarkSlimeBlock>();
        DustType = DustID.UnholyWater;
    }
}
