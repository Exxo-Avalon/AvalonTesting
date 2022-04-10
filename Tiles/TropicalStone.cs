using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class TropicalStone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(234, 234, 234));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<TropicalStoneBlock>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.SnowBlock;
    }
}
