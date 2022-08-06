using System;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;
public class TuhrtlOutpost : GenPass
{
    public TuhrtlOutpost() : base("TuhrtlOutpost", 10)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        int num562 = 0;
        progress.Message = Lang.gen[70].Value;
        long num563 = 0L;
        double num564 = 0.25;
        int y21;
        int x23;
        while (true)
        {
            y21 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 500);
            x23 = (int)(((WorldGen.genRand.NextDouble() * num564 + 0.1) * -WorldGen.dungeonSide + 0.5) * Main.maxTilesX);
            if (Main.tile[x23, y21].HasTile && Main.tile[x23, y21].TileType == ModContent.TileType<Tiles.TropicalGrass>())
            {
                break;
            }
            if (num563++ > 2000000)
            {
                if (num564 == 0.35)
                {
                    num562++;
                    if (num562 > 10)
                    {
                        return;
                    }
                }
                num564 = Math.Min(0.35, num564 + 0.05);
                num563 = 0L;
            }
        }
        AvalonWorld.MakeTempOutpost(x23, y21);
    }
}
