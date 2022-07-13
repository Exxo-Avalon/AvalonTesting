using AvalonTesting.Tiles;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AvalonTesting.World.Passes;

internal class Impvines : GenPass
{
    public Impvines() : base("Impvines", 897.331f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int num586 = 0; num586 < Main.maxTilesX; num586++)
        {
            int num587 = 0;
            for (int num589 = 0; num589 < Main.maxTilesY; num589++)
            {
                if (num587 > 0 && !Main.tile[num586, num589].HasTile)
                {
                    Main.tile[num586, num589].TileType = (ushort)ModContent.TileType<Tiles.Impvines>();
                    Tile t = Main.tile[num586, num589];
                    t.HasTile = true;
                    num587--;
                }
                else
                {
                    num587 = 0;
                }

                if (Main.tile[num586, num589].HasTile &&
                    Main.tile[num586, num589].TileType == (ushort)ModContent.TileType<Impgrass>() &&
                    !Main.tile[num586, num589].BottomSlope && WorldGen.genRand.Next(5) < 3)
                {
                    num587 = WorldGen.genRand.Next(1, 10);
                }
            }
        }
    }
}
