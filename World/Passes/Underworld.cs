using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace AvalonTesting.World.Passes;

class Underworld
{
    public static void Method(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Generating Hellcastle and Ashen Overgrowth";
        World.Structures.HellCastle.Generate(Main.maxTilesX / 3 - 210, Main.maxTilesY - 140); // change back later
        for (int hbx = Main.maxTilesX / 3 - 450; hbx < Main.maxTilesX / 3 + 500; hbx++)
        {
            for (int hby = Main.maxTilesY - 200; hby < Main.maxTilesY - 50; hby++)
            {
                if (Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby - 1].HasTile ||
                    Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby + 1].HasTile ||
                    Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby].HasTile ||
                    Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby].HasTile)
                {
                    if (Main.tile[hbx, hby].TileType == TileID.Ash)
                    {
                        Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<Tiles.Impgrass>();
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            WorldGen.GrowTree(hbx, hby - 1);
                        }
                    }
                }
                //if (WorldGen.genRand.Next(70) == 0)
                //{
                //    WorldGen.OreRunner(hbx, hby, 4, 4, (ushort)ModContent.TileType<Tiles.BrimstoneBlock>());
                //}
            }
        }
    }
}
