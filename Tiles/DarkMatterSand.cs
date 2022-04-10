using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DarkMatterSand : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(126, 71, 107));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSand[Type] = true;
        TileID.Sets.TouchDamageSands[Type] = 15;
        TileID.Sets.Conversion.Sand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.GeneralPlacementTiles[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.TouchDamageSands[Type] = 0;
        TileID.Sets.Falling[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
        ItemDrop = ModContent.ItemType<DarkSandBlock>();
        DustType = ModContent.DustType<DarkMatterDust>();
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
