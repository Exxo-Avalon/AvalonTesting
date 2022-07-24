using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ReLogic.Content;

namespace Avalon.Projectiles.Melee;

public class CaesiumFlail : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Ball");
    }

    public override void SetDefaults()
    {
        Projectile.width = 46;
        Projectile.height = 46;
        Projectile.aiStyle = -1;
        //Projectile.tileCollide = false;
        //AIType = ProjectileID.Mace;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.scale = 1.1f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (Projectile.ai[0] == 0f)
        {
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 vector = targetHitbox.ClosestPointInRect(mountedCenter) - mountedCenter;
            vector.Y /= 0.8f;
            float num = 55f;
            return vector.Length() <= num;
        }
        return false;
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
        //Projectile.ai[0] = 1f;
        if (flag5)
        {
            Projectile.netUpdate = true;
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
        return false;
    }

    public void Flail()
    {
        Player player = Main.player[Projectile.owner];
        if (!player.active || player.dead || player.noItems || player.CCed || Vector2.Distance(Projectile.Center, player.Center) > 900f)
        {
            Projectile.Kill();
            return;
        }
        if (Main.myPlayer == Projectile.owner && Main.mapFullscreen)
        {
            Projectile.Kill();
            return;
        }
        Vector2 mountedCenter = player.MountedCenter;
        bool doFastThrowDust = false;
        bool flag = true;
        bool flag2 = false;
        int launchTimeLimit = 20;
        float launchSpeed = 24f;
        float maxLaunchLength = 1200f;
        float retractAcceleration = 5f;
        float maxRetractSpeed = 16f;
        float forcedRetractAcceleration = 6f;
        float maxForcedRetractSpeed = 48f;
        float unusedRetractAcceleration = 1f;
        float unusedMaxRetractSpeed = 14f;
        int unusedChainLength = 60;
        int defaultHitCooldown = 10;
        int spinHitCooldown = 20;
        int movingHitCooldown = 10;
        int ricochetTimeLimit = launchTimeLimit + 5;
        float meleeSpeed = player.GetAttackSpeed(DamageClass.Melee);
        float num15 = 1f / meleeSpeed;
        launchSpeed *= num15;
        unusedRetractAcceleration *= num15;
        unusedMaxRetractSpeed *= num15;
        retractAcceleration *= num15;
        maxRetractSpeed *= num15;
        forcedRetractAcceleration *= num15;
        maxForcedRetractSpeed *= num15;
        float num16 = launchSpeed * launchTimeLimit;
        float num17 = num16 + 160f;
        Projectile.localNPCHitCooldown = defaultHitCooldown;
        switch ((int)Projectile.ai[0])
        {
            case 0:
            {
                flag2 = true;
                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 origin = mountedCenter;
                    Vector2 mouseWorld = Main.MouseWorld;
                    Vector2 vector3 = origin.DirectionTo(mouseWorld).SafeNormalize(Vector2.UnitX * player.direction);
                    player.ChangeDir((vector3.X > 0f) ? 1 : (-1));
                    if (!player.channel)
                    {
                        Projectile.ai[0] = 1f;
                        Projectile.ai[1] = 0f;
                        Projectile.velocity = vector3 * launchSpeed + player.velocity;
                        Projectile.Center = mountedCenter;
                        Projectile.netUpdate = true;
                        Projectile.ResetLocalNPCHitImmunity();
                        Projectile.localNPCHitCooldown = movingHitCooldown;
                        break;
                    }
                }
                Projectile.localAI[1]++;
                Vector2 vector4 = new Vector2(player.direction).RotatedBy((float)Math.PI * 10f * (Projectile.localAI[1] / 60f) * (float)player.direction);
                vector4.Y *= 0.8f;
                if (vector4.Y * player.gravDir > 0f)
                {
                    vector4.Y *= 0.5f;
                }
                Projectile.Center = mountedCenter + vector4 * 30f;
                Projectile.velocity = Vector2.Zero;
                Projectile.localNPCHitCooldown = spinHitCooldown;
                break;
            }
            case 1:
            {
                //Projectile.tileCollide = true;
                doFastThrowDust = true;
                bool flag4 = Projectile.ai[1]++ >= launchTimeLimit;
                flag4 |= Projectile.Distance(mountedCenter) >= maxLaunchLength;
                if (player.controlUseItem)
                {
                    Projectile.ai[0] = 6f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                    Projectile.velocity *= 0.2f;
                    break;
                }
                if (flag4)
                {
                    Projectile.ai[0] = 2f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                    Projectile.velocity *= 0.3f;
                }
                player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                Projectile.localNPCHitCooldown = movingHitCooldown;
                break;
            }
            case 2:
            {
                Vector2 vector2 = Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                if (Projectile.Distance(mountedCenter) <= maxRetractSpeed)
                {
                    Projectile.Kill();
                    return;
                }
                if (player.controlUseItem)
                {
                    Projectile.ai[0] = 6f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                    Projectile.velocity *= 0.2f;
                }
                else
                {
                    Projectile.velocity *= 0.98f;
                    Projectile.velocity = Projectile.velocity.MoveTowards(vector2 * maxRetractSpeed, retractAcceleration);
                    player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                }
                break;
            }
            case 3:
            {
                if (!player.controlUseItem)
                {
                    Projectile.ai[0] = 4f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                    break;
                }
                float num18 = Projectile.Distance(mountedCenter);
                Projectile.tileCollide = Projectile.ai[1] == 1f;
                bool flag3 = num18 <= num16;
                if (flag3 != Projectile.tileCollide)
                {
                    Projectile.tileCollide = flag3;
                    Projectile.ai[1] = (Projectile.tileCollide ? 1 : 0);
                    Projectile.netUpdate = true;
                }
                if (num18 > (float)unusedChainLength)
                {
                    if (num18 >= num16)
                    {
                        Projectile.velocity *= 0.5f;
                        Projectile.velocity = Projectile.velocity.MoveTowards(Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * unusedMaxRetractSpeed, unusedMaxRetractSpeed);
                    }
                    Projectile.velocity *= 0.98f;
                    Projectile.velocity = Projectile.velocity.MoveTowards(Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * unusedMaxRetractSpeed, unusedRetractAcceleration);
                }
                else
                {
                    if (Projectile.velocity.Length() < 6f)
                    {
                        Projectile.velocity.X *= 0.96f;
                        Projectile.velocity.Y += 0.2f;
                    }
                    if (player.velocity.X == 0f)
                    {
                        Projectile.velocity.X *= 0.96f;
                    }
                }
                player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                break;
            }
            case 4:
            {
                Projectile.tileCollide = false;
                Vector2 vector = Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                if (Projectile.Distance(mountedCenter) <= maxForcedRetractSpeed)
                {
                    Projectile.Kill();
                    return;
                }
                Projectile.velocity *= 0.98f;
                Projectile.velocity = Projectile.velocity.MoveTowards(vector * maxForcedRetractSpeed, forcedRetractAcceleration);
                Vector2 target = Projectile.Center + Projectile.velocity;
                Vector2 value = mountedCenter.DirectionFrom(target).SafeNormalize(Vector2.Zero);
                if (Vector2.Dot(vector, value) < 0f)
                {
                    Projectile.Kill();
                    return;
                }
                player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                break;
            }
            case 5:
                if (Projectile.ai[1]++ >= (float)ricochetTimeLimit)
                {
                    Projectile.ai[0] = 6f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
                else
                {
                    Projectile.localNPCHitCooldown = movingHitCooldown;
                    Projectile.velocity.Y += 0.6f;
                    Projectile.velocity.X *= 0.95f;
                    player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                }
                break;
            case 6:
                if (!player.controlUseItem || Projectile.Distance(mountedCenter) > num17)
                {
                    Projectile.ai[0] = 4f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
                else
                {
                    Projectile.velocity.Y += 0.8f;
                    Projectile.velocity.X *= 0.95f;
                    player.ChangeDir((player.Center.X < Projectile.Center.X) ? 1 : (-1));
                }
                break;
        }
        Projectile.direction = ((Projectile.velocity.X > 0f) ? 1 : (-1));
        Projectile.spriteDirection = Projectile.direction;
        Projectile.ownerHitCheck = flag2;
        if (flag)
        {
            if (Projectile.velocity.Length() > 1f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + Projectile.velocity.X * 0.1f;
            }
            else
            {
                Projectile.rotation += Projectile.velocity.X * 0.1f;
            }
        }
        Projectile.timeLeft = 2;
        player.heldProj = Projectile.whoAmI;
        player.SetDummyItemTime(2);
        player.itemRotation = Projectile.DirectionFrom(mountedCenter).ToRotation();
        if (Projectile.Center.X < mountedCenter.X)
        {
            player.itemRotation += (float)Math.PI;
        }
        player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
        //AI_015_Flails_Dust(doFastThrowDust);
    }

    public override void AI()
    {
        var num250 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CaesiumDust>(), Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 100, default(Color), 1.5f);
        Main.dust[num250].noGravity = true;
        var dust44 = Main.dust[num250];
        dust44.velocity.X /= 2f;
        var dust45 = Main.dust[num250];
        dust45.velocity.Y /= 2f;
        Flail();
        if (Main.player[Projectile.owner].dead)
        {
            Projectile.Kill();
        }
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        SoundEngine.PlaySound(SoundID.Item14, target.position);
        for (int i = 0; i < 5; i++)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 3), Main.rand.Next(-2, 3), ModContent.ProjectileType<Projectiles.CaesiumExplosion>(), damage, 5f, Projectile.owner, 0f, 0f);
        }
        target.AddBuff(BuffID.OnFire, 60 * 5);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Vector2 playerArmPosition = Main.GetPlayerArmPosition(Projectile);

        // This fixes a vanilla GetPlayerArmPosition bug causing the chain to draw incorrectly when stepping up slopes. The flail itself still draws incorrectly due to another similar bug. This should be removed once the vanilla bug is fixed.
        playerArmPosition.Y -= Main.player[Projectile.owner].gfxOffY;

        Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/CaesiumFlail_Chain");
        Asset<Texture2D> chainTextureExtra = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/CaesiumFlail_Chain"); // This texture and related code is optional and used for a unique effect

        Rectangle? chainSourceRectangle = null;
        // Drippler Crippler customizes sourceRectangle to cycle through sprite frames: sourceRectangle = asset.Frame(1, 6);
        float chainHeightAdjustment = 0f; // Use this to adjust the chain overlap.

        Vector2 chainOrigin = chainSourceRectangle.HasValue ? (chainSourceRectangle.Value.Size() / 2f) : (chainTexture.Size() / 2f);
        Vector2 chainDrawPosition = Projectile.Center; //position + new Vector2(23, 23);
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
