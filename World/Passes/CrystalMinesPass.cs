using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;
internal class CrystalMinesPass : GenPass
{
    public CrystalMinesPass() : base("CrystalMines", 10f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        float num611 = Main.maxTilesX * Main.maxTilesY / 5040000f;
        int amtOfBiomes = 3;
        if (Main.maxTilesX == 6300)
            amtOfBiomes = 4;
        if (Main.maxTilesX == 8400)
            amtOfBiomes = 5;
        //int num612 = (int)(WorldGen.genRand.Next(2, 4) * num611);
        float num613 = (Main.maxTilesX - 160) / amtOfBiomes;
        int num614 = 0;
        while (num614 < amtOfBiomes) // amtofbiomes
        {
            float num615 = (float)num614 / amtOfBiomes;
            Point point = WorldGen.RandomRectanglePoint((int)(num615 * (Main.maxTilesX - 160)) + 80, (int)Main.rockLayer + 20, (int)num613, Main.maxTilesY - ((int)Main.rockLayer + 40) - 250);
            //CrystalMinesRunner(point.X, point.Y, 150, 150);
            //Biomes<World.Biomes.CrystalMinesHouseBiome>.Place(new Point(point.X, point.Y), null);
            //num614++;
            WorldGenConfiguration config = WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");
            World.Biomes.CrystalMines crystalMines = config.CreateBiome<Biomes.CrystalMines>();
            if (Biomes.CrystalMinesTest.Place(point))//World.Biomes.CrystalMinesTest
            {
                Biomes.CrystalMinesHouseBiome crystalHouse = config.CreateBiome<Biomes.CrystalMinesHouseBiome>();
                int xpos = WorldGen.genRand.Next(point.X + 20, point.X + 30);
                int ypos = WorldGen.genRand.Next(point.Y + 20, point.Y + 30);
                crystalHouse.Place(new Point(xpos, ypos), null);
                num614++;
            }
        }
    }
}
