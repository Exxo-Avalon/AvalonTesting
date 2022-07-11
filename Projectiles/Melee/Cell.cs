using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ReLogic.Content;

namespace AvalonTesting.Projectiles.Melee;

public class Cell : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Cell");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 22 / 34;
        Projectile.height = dims.Height * 22 / 34 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = ProjAIStyleID.Flail;
        AIType = ProjectileID.Mace;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 1.1f;
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
            Projectile.position.X += Projectile.velocity.X;
            Projectile.velocity.X = -oldVelocity.X * 0.2f;
        }
        if (oldVelocity.Y != Projectile.velocity.Y)
        {
            if (Math.Abs(oldVelocity.Y) > 4f)
            {
                flag5 = true;
            }
            Projectile.position.Y += Projectile.velocity.Y;
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
        if (Main.player[Projectile.owner].dead)
        {
            Projectile.Kill();
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

        // This fixes a vanilla GetPlayerArmPosition bug causing the chain to draw incorrectly when stepping up slopes. The flail itself still draws incorrectly due to another similar bug. This should be removed once the vanilla bug is fixed.
        playerArmPosition.Y -= Main.player[Projectile.owner].gfxOffY;

        Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("AvalonTesting/Projectiles/Melee/Cell_Chain");
        Asset<Texture2D> chainTextureExtra = ModContent.Request<Texture2D>("AvalonTesting/Projectiles/Melee/Cell_Chain"); // This texture and related code is optional and used for a unique effect

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
