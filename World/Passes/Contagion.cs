using System;
using System.Collections.Generic;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.World.Passes;

public class Contagion : GenPass
{
    public Contagion() : base("Contagion", 1094.237f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        int beachBordersWidth = 275;
        int beachSandRandomCenter = beachBordersWidth + 5 + 40;
        int evilBiomeBeachAvoidance = beachSandRandomCenter + 60;
        int evilBiomeAvoidanceMidFixer = 50;

        int jungleTilesUpperX = Main.maxTilesX;
        int jungleTilesLowerX = 0;
        int snowTilesUpperX = Main.maxTilesX;
        int snowTilesLowerX = 0;
        Tile tile;
        for (int x = 0; x < Main.maxTilesX; x++)
        {
            for (int y = 0; y < Main.worldSurface; y++)
            {
                tile = Main.tile[x, y];
                if (tile.HasTile)
                {
                    tile = Main.tile[x, y];
                    if (tile.TileType == TileID.JungleGrass)
                    {
                        if (x < jungleTilesUpperX)
                        {
                            jungleTilesUpperX = x;
                        }

                        if (x > jungleTilesLowerX)
                        {
                            jungleTilesLowerX = x;
                        }
                    }
                    else
                    {
                        tile = Main.tile[x, y];
                        if (tile.TileType is not TileID.SnowBlock and not TileID.IceBlock)
                        {
                            continue;
                        }

                        // Found snow or ice
                        if (x < snowTilesUpperX)
                        {
                            snowTilesUpperX = x;
                        }

                        if (x > snowTilesLowerX)
                        {
                            snowTilesLowerX = x;
                        }
                    }
                }
            }
        }

        int boundaryOffset = 10;
        jungleTilesUpperX -= boundaryOffset;
        jungleTilesLowerX += boundaryOffset;
        snowTilesUpperX -= boundaryOffset;
        snowTilesLowerX += boundaryOffset;

        const int num500 = 500;
        const int num100 = 100;

        bool notDrunk = true;
        double iterations = Main.maxTilesX * 0.00045;
        if (WorldGen.drunkWorldGen)
        {
            iterations /= 2.0;
            if (WorldGen.genRand.Next(2) == 0)
            {
                notDrunk = false;
            }
        }

        progress.Message = "Making the world gross";
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            int snowTilesUpperXInner = snowTilesUpperX;
            int snowTilesLowerXInner = snowTilesLowerX;
            int jungleTilesUpperXInner = jungleTilesUpperX;
            int jungleTilesLowerXInner = jungleTilesLowerX;
            float progressPercent = iteration / (float)iterations;
            progress.Set(progressPercent);
            bool foundGoodSpot = false;
            int randomX = 0;
            int leftBeachAvoidanceCheck = 0;
            int rightBeachAvoidanceCheck = 0;

            while (!foundGoodSpot)
            {
                foundGoodSpot = true;
                int centerX = Main.maxTilesX / 2;
                int minDistanceFromCenter = 200;
                if (WorldGen.drunkWorldGen)
                {
                    minDistanceFromCenter = 100;
                    randomX = !notDrunk
                        ? WorldGen.genRand.Next((int)(Main.maxTilesX * 0.5), Main.maxTilesX - num500)
                        : WorldGen.genRand.Next(num500, (int)(Main.maxTilesX * 0.5));
                }
                else
                {
                    randomX = WorldGen.genRand.Next(num500, Main.maxTilesX - num500);
                }

                leftBeachAvoidanceCheck = randomX - WorldGen.genRand.Next(200) - 100;
                rightBeachAvoidanceCheck = randomX + WorldGen.genRand.Next(200) + 100;
                if (leftBeachAvoidanceCheck < evilBiomeBeachAvoidance)
                {
                    leftBeachAvoidanceCheck = evilBiomeBeachAvoidance;
                }

                if (rightBeachAvoidanceCheck > Main.maxTilesX - evilBiomeBeachAvoidance)
                {
                    rightBeachAvoidanceCheck = Main.maxTilesX - evilBiomeBeachAvoidance;
                }

                if (randomX < leftBeachAvoidanceCheck + evilBiomeAvoidanceMidFixer)
                {
                    randomX = leftBeachAvoidanceCheck + evilBiomeAvoidanceMidFixer;
                }

                if (randomX > rightBeachAvoidanceCheck - evilBiomeAvoidanceMidFixer)
                {
                    randomX = rightBeachAvoidanceCheck - evilBiomeAvoidanceMidFixer;
                }

                if (ModContent.GetInstance<ExxoWorldGen>().DungeonSide < 0 && leftBeachAvoidanceCheck < 400)
                {
                    leftBeachAvoidanceCheck = 400;
                }
                else if (ModContent.GetInstance<ExxoWorldGen>().DungeonSide > 0 &&
                         leftBeachAvoidanceCheck > Main.maxTilesX - 400)
                {
                    leftBeachAvoidanceCheck = Main.maxTilesX - 400;
                }

                if (randomX > centerX - minDistanceFromCenter && randomX < centerX + minDistanceFromCenter)
                {
                    foundGoodSpot = false;
                }

                if (leftBeachAvoidanceCheck > centerX - minDistanceFromCenter &&
                    leftBeachAvoidanceCheck < centerX + minDistanceFromCenter)
                {
                    foundGoodSpot = false;
                }

                if (rightBeachAvoidanceCheck > centerX - minDistanceFromCenter &&
                    rightBeachAvoidanceCheck < centerX + minDistanceFromCenter)
                {
                    foundGoodSpot = false;
                }

                if (randomX > WorldGen.UndergroundDesertLocation.X && randomX <
                    WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
                {
                    foundGoodSpot = false;
                }

                if (leftBeachAvoidanceCheck > WorldGen.UndergroundDesertLocation.X && leftBeachAvoidanceCheck <
                    WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
                {
                    foundGoodSpot = false;
                }

                if (rightBeachAvoidanceCheck > WorldGen.UndergroundDesertLocation.X && rightBeachAvoidanceCheck <
                    WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
                {
                    foundGoodSpot = false;
                }

                if (leftBeachAvoidanceCheck < ModContent.GetInstance<ExxoWorldGen>().DungeonLocation + num100 &&
                    rightBeachAvoidanceCheck > ModContent.GetInstance<ExxoWorldGen>().DungeonLocation - num100)
                {
                    foundGoodSpot = false;
                }

                if (leftBeachAvoidanceCheck < snowTilesLowerXInner && rightBeachAvoidanceCheck > snowTilesUpperXInner)
                {
                    snowTilesUpperXInner++;
                    snowTilesLowerXInner--;
                    foundGoodSpot = false;
                }

                if (leftBeachAvoidanceCheck < jungleTilesLowerXInner &&
                    rightBeachAvoidanceCheck > jungleTilesUpperXInner)
                {
                    jungleTilesUpperXInner++;
                    jungleTilesLowerXInner--;
                    foundGoodSpot = false;
                }
            }

            // Just checked that randomX is valid and now we start
            //ContagionRunner(randomX, (int)WorldGen.worldSurfaceLow - 10);

            // Modify jungle
            for (int x = leftBeachAvoidanceCheck; x < rightBeachAvoidanceCheck; x++)
            {
                for (int y = (int)WorldGen.worldSurfaceLow; y < Main.worldSurface - 1.0; y++)
                {
                    tile = Main.tile[x, y];
                    if (tile.HasTile)
                    {
                        int randomY = y + WorldGen.genRand.Next(10, 14);
                        for (int y2 = y; y2 < randomY; y2++)
                        {
                            tile = Main.tile[x, y2];
                            if (tile.TileType is not TileID.Mud and not TileID.JungleGrass)
                            {
                                continue;
                            }

                            if (x >= leftBeachAvoidanceCheck + WorldGen.genRand.Next(5) &&
                                x < rightBeachAvoidanceCheck - WorldGen.genRand.Next(5))
                            {
                                tile.TileType = TileID.Dirt;
                            }
                        }

                        break;
                    }
                }
            }

            // Replace tiles
            double randomYOffset = Main.worldSurface + 40.0;
            for (int x = leftBeachAvoidanceCheck; x < rightBeachAvoidanceCheck; x++)
            {
                randomYOffset += WorldGen.genRand.Next(-2, 3);
                if (randomYOffset < Main.worldSurface + 30.0)
                {
                    randomYOffset = Main.worldSurface + 30.0;
                }

                if (randomYOffset > Main.worldSurface + 50.0)
                {
                    randomYOffset = Main.worldSurface + 50.0;
                }

                bool unusedFlag = false;
                for (int y = (int)WorldGen.worldSurfaceLow; y < randomYOffset; y++)
                {
                    tile = Main.tile[x, y];
                    if (tile.HasTile)
                    {
                        if (tile.TileType == TileID.Sand && x >= leftBeachAvoidanceCheck + WorldGen.genRand.Next(5) &&
                            x <= rightBeachAvoidanceCheck - WorldGen.genRand.Next(5))
                        {
                            tile.TileType = (ushort)ModContent.TileType<Snotsand>();
                        }

                        tile = Main.tile[x, y];
                        if (tile.TileType == TileID.Dirt && y < Main.worldSurface - 1.0 && !unusedFlag)
                        {
                            WorldGen.SpreadGrass(x, y, TileID.Dirt, ModContent.TileType<Ickgrass>());
                        }

                        unusedFlag = true;
                        if (tile.WallType == WallID.HardenedSand)
                        {
                            // Hardened Snotsand wall
                            //tile.WallType = ModContent.WallType<>()
                        }
                        else if (tile.WallType == WallID.Sandstone)
                        {
                            // Snotsandstone wall
                            //tile.WallType = ModContent.WallType<>()
                        }

                        switch (tile.TileType)
                        {
                            case TileID.Stone:
                            {
                                if (x >= leftBeachAvoidanceCheck + WorldGen.genRand.Next(5) &&
                                    x <= rightBeachAvoidanceCheck - WorldGen.genRand.Next(5))
                                {
                                    tile.TileType = (ushort)ModContent.TileType<Chunkstone>();
                                }

                                break;
                            }
                            case TileID.Grass:
                                tile.TileType = (ushort)ModContent.TileType<Ickgrass>();
                                break;
                            case TileID.IceBlock:
                                tile.TileType = (ushort)ModContent.TileType<YellowIce>();
                                break;
                            case TileID.Sandstone:
                                tile.TileType = (ushort)ModContent.TileType<Snotsandstone>();
                                break;
                            case TileID.HardenedSand:
                                tile.TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                                break;
                        }
                    }
                }
            }

            // Try placing altars
            int altarAttempts = WorldGen.genRand.Next(10, 15);
            for (int i = 0; i < altarAttempts; i++)
            {
                bool finished = false;
                int attemptsAtOffset = 0;
                int offset = 0;
                while (!finished && offset <= 100)
                {
                    attemptsAtOffset++;

                    // Make sure not in the ocean
                    int x = WorldGen.genRand.Next(leftBeachAvoidanceCheck - offset, rightBeachAvoidanceCheck + offset);
                    int y = WorldGen.genRand.Next((int)(Main.worldSurface - (offset / 2.0)),
                        (int)(Main.worldSurface + 100.0 + offset));
                    while (WorldGen.oceanDepths(x, y))
                    {
                        x = WorldGen.genRand.Next(leftBeachAvoidanceCheck - offset, rightBeachAvoidanceCheck + offset);
                        y = WorldGen.genRand.Next((int)(Main.worldSurface - (offset / 2.0)),
                            (int)(Main.worldSurface + 100.0 + offset));
                    }

                    if (attemptsAtOffset > 100)
                    {
                        offset++;
                        attemptsAtOffset = 0;
                    }

                    // Find nearest tile that exists to point and adjust coords
                    tile = Main.tile[x, y];
                    if (!tile.HasTile)
                    {
                        while (true)
                        {
                            tile = Main.tile[x, y];
                            if (tile.HasTile)
                            {
                                break;
                            }

                            y++;
                        }

                        y--;
                    }
                    else
                    {
                        while (true)
                        {
                            tile = Main.tile[x, y];
                            if (!tile.HasTile || !(y > Main.worldSurface))
                            {
                                break;
                            }

                            y--;
                        }
                    }

                    if (offset > 10)
                    {
                        TryPlaceAltar();
                    }

                    tile = Main.tile[x, y + 1];
                    if (tile.HasTile)
                    {
                        tile = Main.tile[x, y + 1];
                        if (tile.TileType == ModContent.TileType<Chunkstone>())
                        {
                            TryPlaceAltar();
                        }
                    }

                    void TryPlaceAltar()
                    {
                        if (!WorldGen.IsTileNearby(x, y, ModContent.TileType<IckyAltar>(), 3))
                        {
                            WorldGen.Place3x2(x, y, (ushort)ModContent.TileType<IckyAltar>());
                            if (Main.tile[x, y].TileType == ModContent.TileType<IckyAltar>())
                            {
                                finished = true;
                            }
                        }
                    }
                }
            }
        }

        //WorldGen.CrimPlaceHearts();
    }
}
