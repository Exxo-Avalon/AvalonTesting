using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class BlastedStone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(20, 20, 20));
        Main.tileSolid[Type] = true;
        Main.tileShine[Type] = 1150;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BlastedStone>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Wraith;
        TileID.Sets.HellSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
