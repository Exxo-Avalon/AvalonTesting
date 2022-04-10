using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class BlackIce : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(127, 104, 135));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<BlackIceBlock>();
        Main.tileMerge[Type][TileID.IceBlock] = true;
        Main.tileMerge[TileID.IceBlock][Type] = true;
        Main.tileShine2[Type] = true;
        DustType = DustID.Clentaminator_Purple;
        SoundType = SoundID.Item;
        SoundStyle = 50;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail)
        {
            AvalonTestingWorld.WorldDarkMatterTiles--;
        }
        base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
    }
}
