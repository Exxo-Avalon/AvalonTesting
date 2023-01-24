using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles;

class TheSun : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The Sun");
    }

    public override void SetDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
        Rectangle dims = this.GetDims();
        Projectile.Size = new Vector2(30);
        Projectile.alpha = 0;
        Projectile.scale = 1.4f;
        Projectile.aiStyle = -1;
        AIType = 20;
        Projectile.timeLeft = 900;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.damage = 0;
        //projectile.ownerHitCheck = true;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.localNPCHitCooldown = 60;
        Projectile.usesLocalNPCImmunity= true;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        //Main.spriteBatch.End();
        //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

        SpriteEffects flip = (Projectile.spriteDirection < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        //Texture2D texture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/Sparkly").Value;
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame(1,4,0,Projectile.frame);
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        Color RealColorHours = Color.White * 0.5f;
        RealColorHours.A = 0;
        for (int i = 1; i <= 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos - Projectile.velocity * i * 0.1f, frame, Color.Lerp(new Color(Main.DiscoR,Main.DiscoG,Main.DiscoB,0),new Color(128,64,0,0),0.5f), Projectile.rotation , frameOrigin, Projectile.scale - (i * 0.1f), flip, 0);
        }
        RealColorHours.A = 128;
        Main.EntitySpriteDraw(texture, drawPos, frame,RealColorHours, Projectile.rotation, frameOrigin, Projectile.scale, flip, 0);

        //unvanilla bad bruh
        //for (int i = 0; i < 30; i++)
        //{
        //    Main.EntitySpriteDraw(texture, drawPos, frame, RealColorHours, Projectile.rotation + Main.rand.NextFloat(0, MathHelper.Pi), frameOrigin, Projectile.scale + (i * 0.01f) * Main.rand.NextFloat(0.7f, 1.5f), flip, 0);
        //}

        //for (int i = 0; i < 30; i++)
        //{
        //    Main.EntitySpriteDraw(texture, drawPos, frame, new Color(RealColorHours.R, RealColorHours.R, RealColorHours.R, 0), Projectile.rotation + Main.rand.NextFloat(0, MathHelper.Pi), frameOrigin, Projectile.scale + (i * 0.01f) * 0.7f * Main.rand.NextFloat(0.7f, 1.5f), flip, 0);
        //}

        return false;
        //return true;
    }
    public override void PostDraw(Color lightColor)
    {
        //Main.spriteBatch.End();
        //Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
    }
    public override void AI()
    {
        Color RealColorHours = Color.Lerp(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0), new Color(60, 60, 60, 0), Main.rand.NextFloat(0.6f,1)) * 0.8f;
        RealColorHours.A = 0;
        int d = Dust.NewDust(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 0.6f, Projectile.width * 0.6f),0,0,DustID.TintableDustLighted);
        Main.dust[d].velocity = Projectile.velocity * -0.3f;
        Main.dust[d].color = RealColorHours;
        Main.dust[d].noGravity = true;
        Main.dust[d].scale = Projectile.scale * 4;
        //Main.dust[d].fadeIn = Projectile.scale * 2.2f;
        Main.dust[d].noLight= true;
        //Main.dust[d].noLightEmittence = true;

        Projectile.rotation += 0.1f;
        #region old
        //Lighting.AddLight(Projectile.position, 255 / 255f, 235 / 255f, 15 / 255f);
        //Projectile.rotation++;
        //Projectile.scale *= 1.002f;
        //if (Projectile.scale > 6f) Projectile.scale = 6f;
        //var v = Projectile.Center - new Vector2(Projectile.width * Projectile.scale / 2f, Projectile.height * Projectile.scale / 2f);
        //var wH = new Vector2(Projectile.width * Projectile.scale, Projectile.height * Projectile.scale);
        //var value2 = ClassExtensions.NewRectVector2(v, wH);
        //var npc = Main.npc;
        //for (var num57 = 0; num57 < npc.Length; num57++)
        //{
        //    var nPC = npc[num57];
        //    if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.life >= 1 && nPC.getRect().Intersects(value2) && Projectile.localNPCImmunity[nPC.whoAmI] == Projectile.localNPCHitCooldown)
        //    {
        //        if (Projectile.ai[0] % 15 == 0) nPC.StrikeNPC(Projectile.damage, Projectile.knockBack, (nPC.Center.X < Projectile.Center.X) ? -1 : 1, false, false);
        //    }
        //}
        //if (Main.rand.Next(2) == 0)
        //{
        //    int dust = Dust.NewDust(new Vector2((float)Projectile.position.X, (float)Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0, 0, 100, Color.White, 3.0f);
        //    Main.dust[dust].noGravity = true;
        //    int dust2 = Dust.NewDust(new Vector2((float)Projectile.position.X, (float)Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, Color.White, 3.0f);
        //    Main.dust[dust2].noGravity = true;
        //}
        #endregion old
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 1)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame >= 4)
        {
            Projectile.frame = 0;
        }
    }
}
