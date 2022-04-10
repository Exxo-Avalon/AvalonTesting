using AvalonTesting.Dusts;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Darksandstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(63, 0, 63));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Sandstone[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.isDesertBiomeSand[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        ItemDrop = ModContent.ItemType<DarksandstoneBlock>();
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
