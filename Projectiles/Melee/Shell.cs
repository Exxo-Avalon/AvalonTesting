using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace Avalon.Projectiles.Melee;

public class Shell : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shell");
        Main.projFrames[Projectile.type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.timeLeft = 900;
        Projectile.damage = 87;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 5f)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.velocity.Y = -oldVelocity.Y * 0.2f;
        }
        if (Projectile.velocity.X != oldVelocity.X)
        {
            Projectile.velocity.X = -oldVelocity.X * 0.85f;
        }
        return false;
    }
    public override void AI()
    {
        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 10 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
        {
            Projectile.velocity.Y += 3.6f;
            Projectile.ai[0] = 0;
        }
        if (Projectile.velocity.Y >= 0f)
        {
            int direction = 0;
            if (Projectile.velocity.X < 0f)
            {
                direction = -1;
            }
            if (Projectile.velocity.X > 0f)
            {
                direction = 1;
            }
            Vector2 posMod = Projectile.position;
            posMod.X += Projectile.velocity.X;
            int xPos = (int)((posMod.X + (float)(Projectile.width / 2) + (float)((Projectile.width / 2 + 1) * direction)) / 16f);
            int yPos = (int)((posMod.Y + (float)Projectile.height - 1f) / 16f);
            if (xPos * 16 < posMod.X + Projectile.width &&
                xPos * 16 + 16 > posMod.X &&
                ((Main.tile[xPos, yPos].HasUnactuatedTile &&
                !Main.tile[xPos, yPos].TopSlope &&
                !Main.tile[xPos, yPos - 1].TopSlope &&
                Main.tileSolid[Main.tile[xPos, yPos].TileType] &&
                !Main.tileSolidTop[Main.tile[xPos, yPos].TileType]) ||
                (Main.tile[xPos, yPos - 1].IsHalfBlock &&
                Main.tile[xPos, yPos - 1].HasUnactuatedTile)) &&
                (!Main.tile[xPos, yPos - 1].HasUnactuatedTile ||
                !Main.tileSolid[Main.tile[xPos, yPos - 1].TileType] ||
                Main.tileSolidTop[Main.tile[xPos, yPos - 1].TileType] || (Main.tile[xPos, yPos - 1].IsHalfBlock && (!Main.tile[xPos, yPos - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 4].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 4].TileType]))) && (!Main.tile[xPos, yPos - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 2].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 2].TileType]) && (!Main.tile[xPos, yPos - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos, yPos - 3].TileType] || Main.tileSolidTop[Main.tile[xPos, yPos - 3].TileType]) && (!Main.tile[xPos - direction, yPos - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[xPos - direction, yPos - 3].TileType]))
            {
                float yPosPixel = yPos * 16;
                if (Main.tile[xPos, yPos].IsHalfBlock)
                {
                    yPosPixel += 8f;
                }
                if (Main.tile[xPos, yPos - 1].IsHalfBlock)
                {
                    yPosPixel -= 8f;
                }
                if (yPosPixel < posMod.Y + Projectile.height)
                {
                    float num101 = posMod.Y + Projectile.height - yPosPixel;
                    float num102 = 16.1f;
                    if (num101 <= num102)
                    {
                        Projectile.gfxOffY += Projectile.position.Y + Projectile.height - yPosPixel;
                        Projectile.position.Y = yPosPixel - Projectile.height;
                        if (num101 < 9f)
                        {
                            Projectile.stepSpeed = 1f;
                        }
                        else
                        {
                            Projectile.stepSpeed = 2f;
                        }
                    }
                }
            }
        }
        Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
        if (Projectile.velocity.Y <= 6f)
        {
            if (Projectile.velocity.X > 0f && Projectile.velocity.X < 7f)
            {
                Projectile.velocity.X = Projectile.velocity.X + 0.05f;
            }
            if (Projectile.velocity.X < 0f && Projectile.velocity.X > -7f)
            {
                Projectile.velocity.X = Projectile.velocity.X - 0.05f;
            }
        }
        Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
        if (Projectile.frameCounter > 10)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 3)
        {
            Projectile.frame = 0;
        }
    }
}
