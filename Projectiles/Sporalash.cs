using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ReLogic.Content;

namespace AvalonTesting.Projectiles;

public class Sporalash : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sporalash");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 22 / 34;
        Projectile.height = dims.Height * 22 / 34 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 0.8f;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        bool flag5 = false;
        if (oldVelocity.X != Projectile.velocity.X)
        {
            if (Math.Abs(oldVelocity.X) > 4f)
            {
                flag5 = true;
            }
            Projectile.position.X = Projectile.position.X + Projectile.velocity.X;
            Projectile.velocity.X = -oldVelocity.X * 0.2f;
        }
        if (oldVelocity.Y != Projectile.velocity.Y)
        {
            if (Math.Abs(oldVelocity.Y) > 4f)
            {
                flag5 = true;
            }
            Projectile.position.Y = Projectile.position.Y + Projectile.velocity.Y;
            Projectile.velocity.Y = -oldVelocity.Y * 0.2f;
        }
        Projectile.ai[0] = 1f;
        if (flag5)
        {
            Projectile.netUpdate = true;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        return false;
    }

    public override void AI()
    {
        var num250 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GrassBlades, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.5f);
        Main.dust[num250].noGravity = true;
        var dust44 = Main.dust[num250];
        dust44.velocity.X = dust44.velocity.X / 2f;
        var dust45 = Main.dust[num250];
        dust45.velocity.Y = dust45.velocity.Y / 2f;
        if (Main.player[Projectile.owner].dead)
        {
            Projectile.Kill();
            return;
        }
        Main.player[Projectile.owner].itemAnimation = 10;
        Main.player[Projectile.owner].itemTime = 10;
        if (Projectile.position.X + Projectile.width / 2 > Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2)
        {
            Main.player[Projectile.owner].ChangeDir(1);
            Projectile.direction = 1;
        }
        else
        {
            Main.player[Projectile.owner].ChangeDir(-1);
            Projectile.direction = -1;
        }
        var vector19 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
        var num253 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector19.X;
        var num254 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector19.Y;
        var num255 = (float)Math.Sqrt(num253 * num253 + num254 * num254);
        if (Projectile.ai[0] == 0f)
        {
            var num256 = 160f;
            Projectile.tileCollide = true;
            if (num255 > num256)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            else if (!Main.player[Projectile.owner].channel)
            {
                if (Projectile.velocity.Y < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.9f;
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 1f;
                Projectile.velocity.X = Projectile.velocity.X * 0.9f;
            }
        }
        else if (Projectile.ai[0] == 1f)
        {
            var num257 = 14f / Main.player[Projectile.owner].GetAttackSpeed(DamageClass.Melee);
            var num258 = 0.9f / Main.player[Projectile.owner].GetAttackSpeed(DamageClass.Melee);
            var num259 = 300f;
            Math.Abs(num253);
            Math.Abs(num254);
            if (Projectile.ai[1] == 1f)
            {
                Projectile.tileCollide = false;
            }
            if (!Main.player[Projectile.owner].channel || num255 > num259 || !Projectile.tileCollide)
            {
                Projectile.ai[1] = 1f;
                if (Projectile.tileCollide)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.tileCollide = false;
                if (num255 < 20f)
                {
                    Projectile.Kill();
                }
            }
            if (!Projectile.tileCollide)
            {
                num258 *= 2f;
            }
            var num260 = 60;
            if (Projectile.type == ProjectileID.FlowerPow)
            {
                num260 = 100;
            }
            if (num255 > num260 || !Projectile.tileCollide)
            {
                num255 = num257 / num255;
                num253 *= num255;
                num254 *= num255;
                new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                var num261 = num253 - Projectile.velocity.X;
                var num262 = num254 - Projectile.velocity.Y;
                var num263 = (float)Math.Sqrt(num261 * num261 + num262 * num262);
                num263 = num258 / num263;
                num261 *= num263;
                num262 *= num263;
                Projectile.velocity.X = Projectile.velocity.X * 0.98f;
                Projectile.velocity.Y = Projectile.velocity.Y * 0.98f;
                Projectile.velocity.X = Projectile.velocity.X + num261;
                Projectile.velocity.Y = Projectile.velocity.Y + num262;
            }
            else
            {
                if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 6f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                }
                if (Main.player[Projectile.owner].velocity.X == 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                }
            }
        }
        Projectile.rotation = (float)Math.Atan2(num254, num253) - Projectile.velocity.X * 0.1f;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

        // This fixes a vanilla GetPlayerArmPosition bug causing the chain to draw incorrectly when stepping up slopes. The flail itself still draws incorrectly due to another similar bug. This should be removed once the vanilla bug is fixed.
        playerArmPosition.Y -= Main.player[Projectile.owner].gfxOffY;

        Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("AvalonTesting/Projectiles/Sporalash_Chain");
        Asset<Texture2D> chainTextureExtra = ModContent.Request<Texture2D>("AvalonTesting/Projectiles/Sporalash_Chain"); // This texture and related code is optional and used for a unique effect

        Rectangle? chainSourceRectangle = null;
        // Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
        float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap. 

        Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
        Vector2 chainDrawPosition = Projectile.Center;
        Vector2 vectorFromProjectileToPlayerArms = playerArmPosition.MoveTowards(chainDrawPosition, 4f) - chainDrawPosition;
        Vector2 unitVectorFromProjectileToPlayerArms = vectorFromProjectileToPlayerArms.SafeNormalize(Vector2.Zero);
        float chainSegmentLength = (chainSourceRectangle.HasValue ? chainSourceRectangle.Value.Height : chainTexture.Height()) + chainHeightAdjustment;
        if (chainSegmentLength == 0)
            chainSegmentLength = 10; // When the chain texture is being loaded, the height is 0 which would cause infinite loops.
        float chainRotation = unitVectorFromProjectileToPlayerArms.ToRotation() + MathHelper.PiOver2;
        int chainCount = 0;
        float chainLengthRemainingToDraw = vectorFromProjectileToPlayerArms.Length() + chainSegmentLength / 2f;

        // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
        while (chainLengthRemainingToDraw > 0f)
        {
            // This code gets the lighting at the current tile coordinates
            Color chainDrawColor = Lighting.GetColor((int)chainDrawPosition.X / 16, (int)(chainDrawPosition.Y / 16f));

            // Flaming Mace and Drippler Crippler use code here to draw custom sprite frames with custom lighting.
            // Cycling through frames: sourceRectangle = asset.Frame(1, 6, 0, chainCount % 6);
            // This example shows how Flaming Mace works. It checks chainCount and changes chainTexture and draw color at different values

            var chainTextureToDraw = chainTexture;
            if (chainCount >= 4)
            {
                // Use normal chainTexture and lighting, no changes
            }
            else if (chainCount >= 2)
            {
                // Near to the ball, we draw a custom chain texture and slightly make it glow if unlit.
                chainTextureToDraw = chainTextureExtra;
                byte minValue = 140;
                if (chainDrawColor.R < minValue)
                    chainDrawColor.R = minValue;

                if (chainDrawColor.G < minValue)
                    chainDrawColor.G = minValue;

                if (chainDrawColor.B < minValue)
                    chainDrawColor.B = minValue;
            }
            else
            {
                // Close to the ball, we draw a custom chain texture and draw it at full brightness glow.
                chainTextureToDraw = chainTextureExtra;
                chainDrawColor = Color.White;
            }

            // Here, we draw the chain texture at the coordinates
            Main.spriteBatch.Draw(chainTextureToDraw.Value, chainDrawPosition - Main.screenPosition, chainSourceRectangle, chainDrawColor, chainRotation, chainOrigin, 1f, SpriteEffects.None, 0f);

            // chainDrawPosition is advanced along the vector back to the player by the chainSegmentLength
            chainDrawPosition += unitVectorFromProjectileToPlayerArms * chainSegmentLength;
            chainCount++;
            chainLengthRemainingToDraw -= chainSegmentLength;
        }

        // Add a motion trail when moving forward, like most flails do (don't add trail if already hit a tile)
        //if (CurrentAIState == AIState.LaunchingForward)
        //{
        //    Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
        //    Vector2 drawOrigin = new Vector2(projectileTexture.Width * 0.5f, Projectile.height * 0.5f);
        //    SpriteEffects spriteEffects = SpriteEffects.None;
        //    if (Projectile.spriteDirection == -1)
        //        spriteEffects = SpriteEffects.FlipHorizontally;
        //    for (int k = 0; k < Projectile.oldPos.Length && k < StateTimer; k++)
        //    {
        //        Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
        //        Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
        //        Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length / 3, spriteEffects, 0f);
        //    }
        //}
        return true;
    }
}
