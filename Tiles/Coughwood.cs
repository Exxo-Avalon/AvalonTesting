using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Coughwood : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(116, 138, 106));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Coughwood>();
        DustType = ModContent.DustType<Dusts.CoughwoodDust>();
    }
}
