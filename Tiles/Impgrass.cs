using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Impgrass : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(238, 102, 70));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBlendAll[Type] = true;
        Main.tileMergeDirt[Type] = true;
        TileID.Sets.NeedsGrassFraming[Type] = true;
        TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Ash;
        ItemDrop = ItemID.AshBlock;
        DustType = DustID.Silt;
    }

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (fail && !effectOnly)
        {
            Main.tile[i, j].TileType = TileID.Ash;
            WorldGen.SquareTileFrame(i, j);
        }
    }
}
