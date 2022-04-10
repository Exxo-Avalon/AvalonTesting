using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class YellowIce : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(234, 204, 0));
        Main.tileBrick[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        ItemDrop = ModContent.ItemType<YellowIceBlock>();
        SoundType = SoundID.Item;
        SoundStyle = 50;
        DustType = ModContent.DustType<ContagionSpray>();
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
    }
}
