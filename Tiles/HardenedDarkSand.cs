﻿using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class HardenedDarkSand : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(63, 0, 63));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.HardenedSand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        ItemDrop = ModContent.ItemType<HardenedDarkSandBlock>();
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
