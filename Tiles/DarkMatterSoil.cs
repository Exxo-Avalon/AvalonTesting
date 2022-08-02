using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DarkMatterSoil : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(111, 77, 124));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlendAll[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<DarkMatterSoilBlock>();
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
