using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Systems;
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
        Main.tileBrick[Type] = true;
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        ItemDrop = ModContent.ItemType<BlackIceBlock>();
        DustType = DustID.Clentaminator_Purple;
        HitSound = SoundID.Item50;
        TileID.Sets.Conversion.Ice[Type] = true;
        TileID.Sets.Ices[Type] = true;
        TileID.Sets.IcesSlush[Type] = true;
        TileID.Sets.IcesSnow[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
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
