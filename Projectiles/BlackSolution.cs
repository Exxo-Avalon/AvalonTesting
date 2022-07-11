using System;
using AvalonTesting.Dusts;
using AvalonTesting.Systems;
using AvalonTesting.Tiles;
using AvalonTesting.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles;

public class BlackSolution : ModProjectile
{
    public int Progress
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Black Spray");
    }

    public override void SetDefaults()
    {
        Projectile.width = 6;
        Projectile.height = 6;
        Projectile.friendly = true;
        Projectile.alpha = 255;
        Projectile.penetrate = -1;
        Projectile.extraUpdates = 2;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
    }

    public override void AI()
    {
        int dustType = ModContent.DustType<BlackSolutionDust>();

        if (Projectile.owner == Main.myPlayer)
        {
            Convert((int)(Projectile.position.X + (Projectile.width * 0.5f)) / 16,
                (int)(Projectile.position.Y + (Projectile.height * 0.5f)) / 16, 2);
        }

        if (Projectile.timeLeft > 133)
        {
            Projectile.timeLeft = 133;
        }

        if (Progress > 7)
        {
            float dustScale = 1f;

            if (Progress == 8)
            {
                dustScale = 0.2f;
            }
            else if (Progress == 9)
            {
                dustScale = 0.4f;
            }
            else if (Progress == 10)
            {
                dustScale = 0.6f;
            }
            else if (Progress == 11)
            {
                dustScale = 0.8f;
            }

            Progress++;

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100);
            Dust dust = Main.dust[dustIndex];
            dust.noGravity = true;
            dust.scale *= 1.75f;
            dust.velocity.X *= 2f;
            dust.velocity.Y *= 2f;
            dust.scale *= dustScale;
        }
        else
        {
            Progress++;
        }

        Projectile.rotation += 0.3f * Projectile.direction;
    }

    private static void Convert(int i, int j, int size = 4)
    {
        for (int k = i - size; k <= i + size; k++)
        {
            for (int l = j - size; l <= j + size; l++)
            {
                if (ModContent.GetInstance<BiomeTileCounts>().WorldDarkMatterTiles >=
                    BiomeTileCounts.DarkMatterTilesHardLimit)
                {
                    return;
                }

                if (WorldGen.InWorld(k, l, 1) &&
                    Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt((size * size) + (size * size)))
                {
                    ushort type = Main.tile[k, l].TileType;
                    ushort wall = Main.tile[k, l].WallType;

                    ushort? replaceType = null;

                    if (WallID.Sets.Conversion.Grass[wall])
                    {
                        replaceType = (ushort)ModContent.WallType<DarkMatterGrassWall>();
                    }
                    else if (WallID.Sets.Conversion.Stone[wall])
                    {
                        replaceType = (ushort)ModContent.WallType<DarkMatterStoneWall>();
                    }
                    else if (wall == WallID.DirtUnsafe)
                    {
                        replaceType = (ushort)ModContent.WallType<DarkMatterSoilWall>();
                    }

                    if (replaceType != null)
                    {
                        Main.tile[k, l].WallType = (ushort)replaceType;
                        WorldGen.SquareWallFrame(k, l);
                        replaceType = null;
                    }

                    if (TileID.Sets.Conversion.Grass[type])
                    {
                        replaceType = (ushort)ModContent.TileType<DarkMatterGrass>();
                    }
                    else if (type is TileID.Dirt or TileID.ClayBlock or TileID.Mud ||
                             type == ModContent.TileType<Loam>())
                    {
                        replaceType = (ushort)ModContent.TileType<DarkMatterSoil>();
                    }
                    else if (TileID.Sets.Conversion.Sand[type])
                    {
                        replaceType = (ushort)ModContent.TileType<DarkMatterSand>();
                    }
                    else if (TileID.Sets.Conversion.Stone[type])
                    {
                        replaceType = (ushort)ModContent.TileType<DarkMatter>();
                    }
                    else if (TileID.Sets.Conversion.Sandstone[type])
                    {
                        replaceType = (ushort)ModContent.TileType<Darksandstone>();
                    }
                    else if (TileID.Sets.Conversion.HardenedSand[type])
                    {
                        replaceType = (ushort)ModContent.TileType<HardenedDarkSand>();
                    }
                    else if (TileID.Sets.Conversion.Ice[type])
                    {
                        replaceType = (ushort)ModContent.TileType<BlackIce>();
                    }

                    if (replaceType != null)
                    {
                        Main.tile[k, l].TileType = (ushort)replaceType;
                        WorldGen.SquareWallFrame(k, l);
                        NetMessage.SendTileSquare(-1, k, l, 1);
                    }
                    else if (wall != Main.tile[k, l].WallType)
                    {
                        NetMessage.SendTileSquare(-1, k, l, 1);
                    }
                }
            }
        }
    }
}
