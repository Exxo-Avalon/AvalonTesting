using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

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
        Main.tileMerge[Type][ModContent.TileType<DarkMatterSoil>()] = true;
        Main.tileMerge[ModContent.TileType<DarkMatterSoil>()][Type] = true;
        TileID.Sets.Conversion.Stone[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.Stone[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<DarkMatterBlock>();
        HitSound = SoundID.Tink;
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
