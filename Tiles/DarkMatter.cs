using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatter : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(135, 15, 170));
        Main.tileShine2[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<DarkMatterBlock>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<DarkMatterDust>();
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!fail)
        {
            ModContent.GetInstance<BiomeTileCounts>().WorldDarkMatterTiles--;
        }

        base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
    }
}
