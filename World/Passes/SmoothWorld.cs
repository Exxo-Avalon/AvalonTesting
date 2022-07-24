using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;

internal class SmoothWorld : GenPass
{
    public SmoothWorld() : base("Unsmoothing Hellcastle", 20f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        int x = (Main.maxTilesX / 5) - 210;
        int y = Main.maxTilesY - 140;
        //int x = Main.maxTilesX / 2;
        //int y = 250;
        for (int i = x; i <= x + 444; i++)
        {
            for (int j = y; j <= y + 99; j++)
            {
                if (Main.tile[i, j].TileType != (ushort)ModContent.TileType<ResistantWoodPlatform>())
                {
                    Tile t = Main.tile[i, j];
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                }
            }
        }
        // unsmoothing hellcastle
    }
}
