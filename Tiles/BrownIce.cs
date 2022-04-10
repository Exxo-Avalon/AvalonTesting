using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class BrownIce : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(210, 147, 128));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<BrownIceBlock>();
        Main.tileShine2[Type] = true;
        SoundType = SoundID.Item;
        SoundStyle = 50;
        DustType = DustID.Dirt;
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
