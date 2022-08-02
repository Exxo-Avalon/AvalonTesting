using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class DarkMatterShortGrass : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileCut[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<DarkMatterGrass>() };
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
        HitSound = SoundID.Grass;
        AddMapEntry(new Color(110, 14, 158));
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        //offsetY = 2;
    }
    //public override void PlaceInWorld(int i, int j, Item item)
    //{
    //    Main.tile[i, j].frameX = (short)(Main.rand.Next(0, 8) * 18);
    //}
}
