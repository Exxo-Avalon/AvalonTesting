using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class GreenIce : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(41, 200, 0));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<GreenIceBlock>();
        Main.tileShine2[Type] = true;
        SoundType = SoundID.Item;
        SoundStyle = 50;
        DustType = DustID.TerraBlade;
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
