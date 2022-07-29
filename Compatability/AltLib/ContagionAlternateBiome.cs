using System;
using System.Collections.Generic;
using AltLibrary;
using AltLibrary.Common.AltBiomes;
using AltLibrary.Core.Generation;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Weapons.Melee;
using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.AltLib;

internal class ContagionAlternateBiome : AltBiome
{
    public override Color NameColor => new(0, 255, 128);
    private int xLoc;
    public override void SetStaticDefaults()
    {
        BiomeType = BiomeType.Evil;

        BiomeGrass = ModContent.TileType<Ickgrass>();
        BiomeStone = ModContent.TileType<Chunkstone>();
        BiomeSand = ModContent.TileType<Snotsand>();
        BiomeIce = ModContent.TileType<YellowIce>();
        BiomeSandstone = ModContent.TileType<Snotsandstone>();
        BiomeHardenedSand = ModContent.TileType<HardenedSnotsand>();
        BiomeOre = ModContent.TileType<PandemiteOre>();
        BiomeOreBrick = ModContent.TileType<ChunkstoneBrick>(); //Change to Baccilite Brick when its finished
        AltarTile = ModContent.TileType<IckyAltar>();
        //BiomeGrassWall = ModContent.WallType<ContagionGrassWall>();

        GenPassName.SetDefault("Making the world gross");
        EvilBiomeGenerationPass = new ContagionGeneration();

        BiomeOreItem = ModContent.ItemType<Items.Placeable.Tile.PandemiteOre>();
        SeedType = ModContent.ItemType<IckgrassSeeds>();

        BiomeChestItem = ModContent.ItemType<VirulentScythe>();
        BiomeChestTile = ModContent.TileType<LockedContagionChest>();
        BiomeChestTileStyle = 0;

        /*MimicKeyType = ItemID.NightKey;
        MimicType = ModContent.NPCType<ContagionMimic>();
        BloodBunny = ModContent.NPCType<InfectedBunny>();
        BloodGoldfish = ModContent.NPCType<InfectedGoldfish>();
        BloodPenguin = ModContent.NPCType<InfectedPenguin>();*/

        DisplayName.SetDefault("Contagion");
        Description.SetDefault("An infested green biome that consists of a disgusting landscape filled with vile giant bacteria");

        /*WallContext = new WallContext()
            .AddReplacement<ContagionNaturalWall1>(28, 1, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 188, 189, 190, 191, 192, 193, 194, 195, 61, 185, 212, 213, 214, 215, 3, 200, 201, 202, 203, 83)
            .AddReplacement<ContagionGrassWall>(63, 65, 66, 68, 69, 70, 81)
            .AddReplacement<ContagionNaturalWall2>(216, 217, 218, 219) //Sandstone walls
            .AddReplacement<ContagionNaturalWall2>(197, 220, 221, 222); //Hardened sand walls*/
    }
    public override EvilBiomeGenerationPass GetEvilBiomeGenerationPass() => new ContagionGeneration();
    public override List<int> SpreadingTiles => new() { ModContent.TileType<Ickgrass>(), ModContent.TileType<Chunkstone>(), ModContent.TileType<Snotsand>(), ModContent.TileType<GreenIce>(), ModContent.TileType<Snotsandstone>(), ModContent.TileType<HardenedSnotsand>() };

    public override string WorldIcon => "Avalon/Assets/WorldIcons/Contagion";

    public override string IconSmall => "Avalon/Assets/Bestiary/ContagionIcon";

    public override string IconLarge => "Avalon/Assets/Textures/UI/ContagionPreview";

    public override string OuterTexture => "Avalon/Assets/Loading/OuterContagion";

    public override Color OuterColor => new(175, 148, 199);
}

public class ContagionGeneration : EvilBiomeGenerationPass
{
    public override string ProgressMessage => "Making the world gross";
    public override bool CanGenerateNearDungeonOcean => false;

    private const int beachBordersWidth = 275;
    private const int beachSandRandomCenter = beachBordersWidth + 5 + 40;
    private const int evilBiomeBeachAvoidance = beachSandRandomCenter + 60;

    public override void GetEvilSpawnLocation(int dungeonSide, int dungeonLocation, int SnowBoundMinX, int SnowBoundMaxX, int JungleBoundMinX, int JungleBoundMaxX, int currentDrunkIter, int maxDrunkBorders, out int evilBiomePosition, out int evilBiomePositionWestBound, out int evilBiomePositionEastBound)
    {
        bool FoundEvilLocation = false;
        evilBiomePosition = 0;
        evilBiomePositionWestBound = 0;
        evilBiomePositionEastBound = 0;

        while (!FoundEvilLocation)
        {
            FoundEvilLocation = true;
            int MapCenter = Main.maxTilesX / 2;
            int MapCenterGive = 200;

            if (WorldGen.drunkWorldGen)
            {
                MapCenterGive = DrunkRNGMapCenterGive;

                int diff = Main.maxTilesX - NonDrunkBorderDist - NonDrunkBorderDist;

                int left = NonDrunkBorderDist + diff * currentDrunkIter / maxDrunkBorders;
                int right = NonDrunkBorderDist + diff * (currentDrunkIter + 1) / maxDrunkBorders;

                evilBiomePosition = WorldGen.genRand.Next(left, right);

                /*
                if (drunkRNGTilt)
                    evilBiomePosition = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.5), Main.maxTilesX - nonDrunkBorderDist);
                else
                    evilBiomePosition = WorldGen.genRand.Next(nonDrunkBorderDist, (int)((double)Main.maxTilesX * 0.5));*/
            }
            else
            {
                evilBiomePosition = WorldGen.genRand.Next(NonDrunkBorderDist, Main.maxTilesX - NonDrunkBorderDist);
            }
            evilBiomePositionWestBound = evilBiomePosition - WorldGen.genRand.Next(200) - 100;
            evilBiomePositionEastBound = evilBiomePosition + WorldGen.genRand.Next(200) + 100;

            if (evilBiomePositionWestBound < evilBiomeBeachAvoidance)
            {
                evilBiomePositionWestBound = evilBiomeBeachAvoidance;
            }
            if (evilBiomePositionEastBound > Main.maxTilesX - evilBiomeBeachAvoidance)
            {
                evilBiomePositionEastBound = Main.maxTilesX - evilBiomeBeachAvoidance;
            }
            if (evilBiomePosition < evilBiomePositionWestBound + EvilBiomeAvoidanceMidFixer)
            {
                evilBiomePosition = evilBiomePositionWestBound + EvilBiomeAvoidanceMidFixer;
            }
            if (evilBiomePosition > evilBiomePositionEastBound - EvilBiomeAvoidanceMidFixer)
            {
                evilBiomePosition = evilBiomePositionEastBound - EvilBiomeAvoidanceMidFixer;
            }
            //DIFFERENCE 2 - CRIMSON ONLY
            if (!CanGenerateNearDungeonOcean)
            {
                if (dungeonSide < 0 && evilBiomePositionWestBound < 400)
                {
                    evilBiomePositionWestBound = 400;
                }
                else if (dungeonSide > 0 && evilBiomePositionWestBound > Main.maxTilesX - 400)
                {
                    evilBiomePositionWestBound = Main.maxTilesX - 400;
                }
            }
            //DIFFERENCE 2 END
            if (evilBiomePosition > MapCenter - MapCenterGive && evilBiomePosition < MapCenter + MapCenterGive)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionWestBound > MapCenter - MapCenterGive && evilBiomePositionWestBound < MapCenter + MapCenterGive)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionEastBound > MapCenter - MapCenterGive && evilBiomePositionEastBound < MapCenter + MapCenterGive)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePosition > WorldGen.UndergroundDesertLocation.X && evilBiomePosition < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionWestBound > WorldGen.UndergroundDesertLocation.X && evilBiomePositionWestBound < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionEastBound > WorldGen.UndergroundDesertLocation.X && evilBiomePositionEastBound < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionWestBound < dungeonLocation + DungeonGive && evilBiomePositionEastBound > dungeonLocation - DungeonGive)
            {
                FoundEvilLocation = false;
            }
            if (evilBiomePositionWestBound < SnowBoundMinX && evilBiomePositionEastBound > SnowBoundMaxX)
            {
                SnowBoundMinX++;
                SnowBoundMaxX--;
                FoundEvilLocation = false;
            }
            //if (evilBiomePositionWestBound > JungleBoundMinX)
            //{
            //    evilBiomePositionWestBound--;
            //}
            //if (evilBiomePositionEastBound > JungleBoundMaxX)
            //{
            //    evilBiomePositionEastBound++;
            //}
            if (evilBiomePositionWestBound < JungleBoundMinX && evilBiomePositionEastBound > JungleBoundMaxX)
            {
                JungleBoundMinX++;
                JungleBoundMaxX--;
                FoundEvilLocation = false;
            }
        }
        GenerateEvil2(evilBiomePosition, evilBiomePositionWestBound, evilBiomePositionEastBound);
    }

    public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
    {

    }

    public void GenerateEvil2(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
    {
        int radius = WorldGen.genRand.Next(50, 61);
        int rad2 = WorldGen.genRand.Next(20, 26);
        int j = World.Utils.TileCheck(evilBiomePosition) + radius + 50;
        var center = new Vector2(evilBiomePosition, j);
        var points = new List<Vector2>();
        var pointsToGoTo = new List<Vector2>();
        var angles = new List<double>();
        var outerCircles = new List<Vector2>(); // the circles at the ends of the first tunnels
        var secondaryCircles = new List<Vector2>(); // the circles at the ends of the outer circles
        var secondCircleStartPoints = new List<Vector2>();
        var secondCircleEndpoints = new List<Vector2>();
        var secondCirclePointsAroundCircle = new List<double>();
        var exclusions = new List<Vector2>();
        var excludedPointsForOuterTunnels = new List<Vector2>();
        //new List<Vector2>();

        #region make the main circle

        for (int k = evilBiomePosition - radius; k <= evilBiomePosition + radius; k++)
        {
            for (int l = j - radius; l <= j + radius; l++)
            {
                float dist = Vector2.Distance(new Vector2(k, l), new Vector2(evilBiomePosition, j));
                if (dist <= radius && dist >= radius - 29)
                {
                    Main.tile[k, l].Active(false);
                }

                if (((dist <= radius && dist >= radius - 7) || (dist <= radius - 22 && dist >= radius - 29)) &&
                    Main.tile[k, l].TileType != (ushort)ModContent.TileType<SnotOrb>())
                {
                    Main.tile[k, l].Active(true);
                    Tile tile2 = Main.tile[k, l];
                    tile2.IsHalfBlock = false;
                    tile2.Slope = SlopeType.Solid;
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Chunkstone>();
                }

                if (dist <= radius - 6 && dist >= radius - 23)
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
            }
        }

        #endregion

        int radiusModifier =
            radius - 7; // makes the tunnels go deeper into the main circle (more subtracted means further in)
        Vector2 posToPlaceAnotherCircle = Vector2.Zero;

        #region find the points for making the tunnels to the outer circles

        int fiftyRand = WorldGen.genRand.Next(30, 60);
        int tfRand = WorldGen.genRand.Next(15, 35);

        for (int m = 0; m < 16; m++)
        {
            double positionAroundCircle = WorldGen.genRand.Next(0, 62831852) / 10000000;
            var randPoint = new Vector2(center.X + (int)Math.Round(radiusModifier * Math.Cos(positionAroundCircle)),
                center.Y + (int)Math.Round(radiusModifier * Math.Sin(positionAroundCircle)));
            posToPlaceAnotherCircle = randPoint;
            Vector2 item2 = center;

            if (randPoint.X > center.X)
            {
                if (randPoint.X > center.X + (radius / 2))
                {
                    if (randPoint.Y > center.Y)
                    {
                        if (randPoint.Y > center.Y + (radius / 2))
                        {
                            item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y + fiftyRand);
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                outerCircles.Add(item2);
                                secondaryCircles.Add(item2);
                                excludedPointsForOuterTunnels.Add(randPoint);
                            }
                        }
                        else
                        {
                            item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y + tfRand);
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                outerCircles.Add(item2);
                                secondaryCircles.Add(item2);
                                excludedPointsForOuterTunnels.Add(randPoint);
                            }
                        }
                    }
                    else if (randPoint.Y < center.Y - (radius / 2))
                    {
                        item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y - fiftyRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y - tfRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y > center.Y)
                {
                    if (randPoint.Y > center.Y + (radius / 2))
                    {
                        item2 = new Vector2(randPoint.X + tfRand, randPoint.Y + fiftyRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X + tfRand, randPoint.Y + tfRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y < center.Y - (radius / 2))
                {
                    item2 = new Vector2(randPoint.X + tfRand, randPoint.Y - fiftyRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X + tfRand, randPoint.Y - tfRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.X < center.X - (radius / 2))
            {
                if (randPoint.Y > center.Y)
                {
                    if (randPoint.Y > center.Y + (radius / 2))
                    {
                        item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y + fiftyRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y + tfRand);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y < center.Y - (radius / 2))
                {
                    item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y - fiftyRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y - tfRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.Y > center.Y)
            {
                if (randPoint.Y > center.Y + (radius / 2))
                {
                    item2 = new Vector2(randPoint.X - tfRand, randPoint.Y + fiftyRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X - tfRand, randPoint.Y + tfRand);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.Y < center.Y - (radius / 2))
            {
                item2 = new Vector2(randPoint.X - tfRand, randPoint.Y - fiftyRand);
                if (WorldGen.genRand.Next(2) == 0)
                {
                    outerCircles.Add(item2);
                    secondaryCircles.Add(item2);
                    excludedPointsForOuterTunnels.Add(randPoint);
                }
            }
            else
            {
                item2 = new Vector2(randPoint.X - tfRand, randPoint.Y - tfRand);
                if (WorldGen.genRand.Next(2) == 0)
                {
                    outerCircles.Add(item2);
                    secondaryCircles.Add(item2);
                    excludedPointsForOuterTunnels.Add(randPoint);
                }
            }

            points.Add(randPoint);
            pointsToGoTo.Add(item2);
            angles.Add(positionAroundCircle);
        }

        #endregion

        // make outer circles

        #region outer circles and tunnels

        if (secondaryCircles.Count != 0)
        {
            for (int z = 0; z < secondaryCircles.Count; z++)
            {
                if (secondaryCircles[z].Y < center.Y - 10)
                {
                    continue;
                }

                int outerTunnelsRadiusMod = rad2 - 6;
                double pointsAroundCircle2 = WorldGen.genRand.Next(0, 62831852) / 10000000;
                var randPointAroundCircle =
                    new Vector2(
                        outerCircles[z].X + (int)Math.Round(outerTunnelsRadiusMod * Math.Cos(pointsAroundCircle2)),
                        outerCircles[z].Y + (int)Math.Round(outerTunnelsRadiusMod * Math.Sin(pointsAroundCircle2)));
                int fifteenRand = WorldGen.genRand.Next(-15, 15);
                int sevenRand = WorldGen.genRand.Next(-7, 7);
                for (int m = 0; m < 16; m++)
                {
                    Vector2 endpoint = secondaryCircles[z];

                    #region endpoint calculation

                    if (randPointAroundCircle.X > outerCircles[z].X)
                    {
                        if (randPointAroundCircle.X > outerCircles[z].X + (rad2 / 2))
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y)
                            {
                                if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f,
                                        randPointAroundCircle.Y + 15f);
                                }
                                else
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y + 7f);
                                }
                            }
                            else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.X < outerCircles[z].X - (rad2 / 2))
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y > outerCircles[z].Y)
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 15f);
                    }
                    else
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 7f);
                    }

                    #endregion

                    secondCircleStartPoints.Add(randPointAroundCircle);
                    secondCircleEndpoints.Add(endpoint);
                    secondCirclePointsAroundCircle.Add(pointsAroundCircle2);
                }
            }
        }

        #endregion

        // make tunnels going outwards from the main circle
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10)
            {
                continue;
            }

            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 10f,
                (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 5f, 65535);
            MakeEndingCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 13f,
                (ushort)ModContent.TileType<Chunkstone>());
            MakeCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 8f, 65535);
        }

        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10)
                {
                    continue;
                }

                MakeEndingCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2,
                    (ushort)ModContent.TileType<Chunkstone>());
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13,
                    (ushort)ModContent.TileType<Chunkstone>());
                exclusions.Add(outerCircles[q]);
            }
        }

        int num8 = radius - 7;
        for (int num9 = 0; num9 < 20; num9++)
        {
            double d = WorldGen.genRand.Next(0, 62831852) / 10000000;
            var vector2 = new Vector2(center.X + (int)Math.Round(num8 * Math.Cos(d)),
                center.Y + (int)Math.Round(num8 * Math.Sin(d)));
            if (exclusions.Contains(vector2))
            {
                continue;
            }

            MakeCircle((int)vector2.X, (int)vector2.Y, 4f, (ushort)ModContent.TileType<Chunkstone>());
        }

        // make tunnels going outwards from the outer circles
        for (int n = 0; n < secondCircleStartPoints.Count; n++)
        {
            if (excludedPointsForOuterTunnels.Count != 0 && n < excludedPointsForOuterTunnels.Count)
            {
                if (Vector2.Distance(excludedPointsForOuterTunnels[n], secondCircleEndpoints[n]) < 55)
                {
                    continue;
                }
            }

            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y,
                (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 7f,
                (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y,
                (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535);
            // ending circles
            MakeCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535); // air
            MakeEndingCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 5f,
                (ushort)ModContent.TileType<Chunkstone>()); // chunkstone
        }

        // fill main tunnels with air
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10)
            {
                exclusions.Add(pointsToGoTo[n]);
                continue;
            }

            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 3f, 65535);
        }

        // make secondary circles inner area filled
        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10)
                {
                    continue;
                }

                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13,
                    (ushort)ModContent.TileType<Chunkstone>());
            }
        }

        for (int num5 = evilBiomePosition - radius; num5 <= evilBiomePosition + radius; num5++)
        {
            for (int num6 = j - radius; num6 <= j + radius; num6++)
            {
                float num7 = Vector2.Distance(new Vector2(num5, num6), new Vector2(evilBiomePosition, j));
                if (num7 < radius - 7 && num7 > radius - 22)
                {
                    Main.tile[num5, num6].Active(false);
                }
            }
        }

        for (int num10 = 0; num10 < pointsToGoTo.Count; num10++)
        {
            if (exclusions.Contains(pointsToGoTo[num10]))
            {
                continue;
            }

            AddSnotOrb((int)pointsToGoTo[num10].X, (int)pointsToGoTo[num10].Y);
        }

        for (int num10 = 0; num10 < secondCircleEndpoints.Count; num10++)
        {
            if (exclusions.Contains(secondCircleEndpoints[num10]))
            {
                continue;
            }

            AddSnotOrb((int)secondCircleEndpoints[num10].X, (int)secondCircleEndpoints[num10].Y);
        }

        BoreTunnel2(evilBiomePosition, j - radius - 50, evilBiomePosition, j - radius + 7, 5, ushort.MaxValue);
        for (int x = evilBiomePosition - 12; x < evilBiomePosition + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x >= evilBiomePosition + 7 || x <= evilBiomePosition - 7)
                {
                    Main.tile[x, y].Active(true);
                    Tile tile3 = Main.tile[x, y];
                    tile3.IsHalfBlock = false;
                    tile3.Slope = SlopeType.Solid;
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<Chunkstone>();
                }

                if (x <= evilBiomePosition + 7 && x >= evilBiomePosition - 7)
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    Main.tile[x, y].Active(false);
                }
            }
        }

        for (int x = evilBiomePosition - 12; x < evilBiomePosition + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x == evilBiomePosition + 9 || x == evilBiomePosition - 9)
                {
                    int rn = WorldGen.genRand.Next(13, 17);
                    if (y % rn == 0)
                    {
                        MakeCircle(x, y, 3, (ushort)ModContent.TileType<Chunkstone>());
                    }
                }
            }
        }

        /*double num22 = Main.worldSurface + 40.0;
        for (int l = evilBiomePositionWestBound; l < evilBiomePositionEastBound; l++)
        {
            num22 += WorldGen.genRand.Next(-2, 3);
            if (num22 < Main.worldSurface + 30.0)
            {
                num22 = Main.worldSurface + 30.0;
            }
            if (num22 > Main.worldSurface + 50.0)
            {
                num22 = Main.worldSurface + 50.0;
            }
            int i2 = l;
            bool flag4 = false;
            int num23 = (int)WorldGen.worldSurfaceLow;
            while (num23 < num22)
            {
                if (Main.tile[i2, num23].HasTile)
                {
                    if (Main.tile[i2, num23].TileType == TileID.Sand && i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Snotsand>();
                    }
                    if (Main.tile[i2, num23].TileType == TileID.Dirt && num23 < Main.worldSurface - 1.0 && !flag4)
                    {
                        //ALReflection.WorldGen_GrassSpread = 0;
                        WorldGen.SpreadGrass(i2, num23, 0, (ushort)ModContent.TileType<Ickgrass>(), true, 0);
                    }
                    flag4 = true;
                    if (Main.tile[i2, num23].WallType == WallID.HardenedSand)
                    {
                        Main.tile[i2, num23].WallType = (ushort)ModContent.WallType<HardenedSnotsandWall>();
                    }
                    else if (Main.tile[i2, num23].WallType == WallID.Sandstone)
                    {
                        Main.tile[i2, num23].WallType = (ushort)ModContent.WallType<SnotsandstoneWall>();
                    }
                    if (Main.tile[i2, num23].TileType == TileID.Stone)
                    {
                        if (i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                        {
                            Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Chunkstone>();
                        }
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.Grass)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Ickgrass>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.IceBlock)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<YellowIce>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.Sandstone)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.HardenedSand)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                    }
                }
                num23++;
            }
        }
        int num24 = WorldGen.genRand.Next(10, 15);
        for (int m = 0; m < num24; m++)
        {
            int num25 = 0;
            bool flag5 = false;
            int num26 = 0;
            while (!flag5)
            {
                num25++;
                int x = WorldGen.genRand.Next(evilBiomePositionWestBound - num26, evilBiomePositionEastBound + num26);
                int num27 = WorldGen.genRand.Next((int)(Main.worldSurface - num26 / 2), (int)(Main.worldSurface + 100.0 + num26));
                while (WorldGen.oceanDepths(x, num27))
                {
                    x = WorldGen.genRand.Next(evilBiomePositionWestBound - num26, evilBiomePositionEastBound + num26);
                    num27 = WorldGen.genRand.Next((int)(Main.worldSurface - num26 / 2), (int)(Main.worldSurface + 100.0 + num26));
                }
                if (num25 > 100)
                {
                    num26++;
                    num25 = 0;
                }
                if (!Main.tile[x, num27].HasTile)
                {
                    while (!Main.tile[x, num27].HasTile)
                    {
                        num27++;
                    }
                    num27--;
                }
                else
                {
                    while (Main.tile[x, num27].HasTile && num27 > Main.worldSurface)
                    {
                        num27--;
                    }
                }
                if ((num26 > 10 || Main.tile[x, num27 + 1].HasTile && Main.tile[x, num27 + 1].TileType == 203) && !WorldGen.IsTileNearby(x, num27, ModContent.TileType<IckyAltar>(), 3))
                {
                    WorldGen.Place3x2(x, num27, (ushort)ModContent.TileType<IckyAltar>());
                    if (Main.tile[x, num27].TileType == ModContent.TileType<IckyAltar>())
                    {
                        flag5 = true;
                    }
                }
                if (num26 > 100)
                {
                    flag5 = true;
                }
            }
        }*/
    }
    /// <summary>
    ///     A helper method to generate a tunnel using MakeCircle().
    /// </summary>
    /// <param name="x0">Starting x coordinate.</param>
    /// <param name="y0">Starting y coordinate.</param>
    /// <param name="x1">Ending x coordinate.</param>
    /// <param name="y1">Ending y coordinate.</param>
    /// <param name="r">Radius.</param>
    /// <param name="type">Type to generate.</param>
    public static void BoreTunnel2(int x0, int y0, int x1, int y1, float r, ushort type)
    {
        bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (flag)
        {
            Utils.Swap(ref x0, ref y0);
            Utils.Swap(ref x1, ref y1);
        }

        if (x0 > x1)
        {
            Utils.Swap(ref x0, ref x1);
            Utils.Swap(ref y0, ref y1);
        }

        int num = x1 - x0;
        int num2 = Math.Abs(y1 - y0);
        int num3 = num / 2;
        int num4 = y0 < y1 ? 1 : -1;
        int num5 = y0;
        for (int i = x0; i <= x1; i++)
        {
            if (flag)
            {
                MakeCircle(num5, i, r, type);
            }
            else
            {
                MakeCircle(i, num5, r, type);
            }

            num3 -= num2;
            if (num3 < 0)
            {
                num5 += num4;
                num3 += num;
            }
        }
    }

    /// <summary>
    ///     Makes a circle for the Contagion generation. Fills all tiles with Chunkstone Walls.
    /// </summary>
    /// <param name="x">The x coordinate of the center of the circle.</param>
    /// <param name="y">The y coordinate of the center of the circle.</param>
    /// <param name="r">The radius of the circle.</param>
    /// <param name="type">The type to generate - if ushort.MaxValue, will generate air.</param>
    public static void MakeCircle(int x, int y, float r, ushort type)
    {
        int num = (int)(x - r);
        int num2 = (int)(y - r);
        int num3 = (int)(x + r);
        int num4 = (int)(y + r);
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r &&
                    Main.tile[i, j].TileType != TileID.ShadowOrbs)
                {
                    if (type == 65535)
                    {
                        Main.tile[i, j].Active(false);
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        Main.tile[i, j].Active(true);
                        Main.tile[i, j].TileType = type;
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
                //else if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) == r - 1)
                //{
                //    Main.tile[i, j].wall = 0;
                //}
            }
        }
    }

    /// <summary>
    ///     Makes an ending circle for the Contagion generation.
    /// </summary>
    /// <param name="x">The x coordinate of the center of the circle.</param>
    /// <param name="y">The y coordinate of the center of the circle.</param>
    /// <param name="r">The radius of the circle.</param>
    /// <param name="type">The type to generate - if ushort.MaxValue, will generate air.</param>
    public static void MakeEndingCircle(int x, int y, float r, ushort type)
    {
        int num = (int)(x - r);
        int num2 = (int)(y - r);
        int num3 = (int)(x + r);
        int num4 = (int)(y + r);
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r &&
                    Main.tile[i, j].TileType != TileID.ShadowOrbs)
                {
                    if (type == 65535)
                    {
                        Main.tile[i, j].Active(false);
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        Main.tile[i, j].Active(true);
                        Main.tile[i, j].TileType = type;
                        //Main.tile[i, j].wall = (ushort)ModContent.WallType<Walls.ChunkstoneWall>();
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
                else if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) == r - 1)
                {
                    Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
            }
        }
    }

    /// <summary>
    ///     Adds a Snot Orb at the given coordinates. For the Contagion.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="style">Unused.</param>
    public static void AddSnotOrb(int x, int y, int style = 0)
    {
        if (x < 10 || x > Main.maxTilesX - 10)
        {
            return;
        }

        if (y < 10 || y > Main.maxTilesY - 10)
        {
            return;
        }

        for (int i = x - 1; i < x + 1; i++)
        {
            for (int j = y - 1; j < y + 1; j++)
            {
                if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == (ushort)ModContent.TileType<SnotOrb>())
                {
                    return;
                }
            }
        }

        short num = 0;
        Main.tile[x - 1, y - 1].Active(true);
        Main.tile[x - 1, y - 1].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x - 1, y - 1].TileFrameX = num;
        Main.tile[x - 1, y - 1].TileFrameY = 0;
        Main.tile[x, y - 1].Active(true);
        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x, y - 1].TileFrameX = (short)(18 + num);
        Main.tile[x, y - 1].TileFrameY = 0;
        Main.tile[x - 1, y].Active(true);
        Main.tile[x - 1, y].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x - 1, y].TileFrameX = num;
        Main.tile[x - 1, y].TileFrameY = 18;
        Main.tile[x, y].Active(true);
        Main.tile[x, y].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x, y].TileFrameX = (short)(18 + num);
        Main.tile[x, y].TileFrameY = 18;
    }
    public override void PostGenerateEvil()
    {

    }
}
