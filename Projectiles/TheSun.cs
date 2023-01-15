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
        Projectile.width = dims.Width * 30 / 40;
        Projectile.height = dims.Height * 30 / 40 / Main.projFrames[Projectile.type];
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
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
        return true;
    }
    public override void PostDraw(Color lightColor)
    {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
    }
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 255 / 255f, 235 / 255f, 15 / 255f);
        Projectile.rotation++;
        Projectile.scale *= 1.002f;
        if (Projectile.scale > 6f) Projectile.scale = 6f;
        var v = Projectile.Center - new Vector2(Projectile.width * Projectile.scale / 2f, Projectile.height * Projectile.scale / 2f);
        var wH = new Vector2(Projectile.width * Projectile.scale, Projectile.height * Projectile.scale);
        var value2 = ClassExtensions.NewRectVector2(v, wH);
        var npc = Main.npc;
        for (var num57 = 0; num57 < npc.Length; num57++)
        {
            var nPC = npc[num57];
            if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.life >= 1 && nPC.getRect().Intersects(value2))
            {
                if (Projectile.ai[0] % 15 == 0) nPC.StrikeNPC(Projectile.damage, Projectile.knockBack, (nPC.Center.X < Projectile.Center.X) ? -1 : 1, false, false);
            }
        }
        if (Main.rand.Next(2) == 0)
        {
            int dust = Dust.NewDust(new Vector2((float)Projectile.position.X, (float)Projectile.position.Y), Projectile.width, Projectile.height, DustID.HallowedWeapons, 0, 0, 100, Color.White, 3.0f);
            Main.dust[dust].noGravity = true;
            int dust2 = Dust.NewDust(new Vector2((float)Projectile.position.X, (float)Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, Color.White, 3.0f);
            Main.dust[dust2].noGravity = true;
        }

        Projectile.frameCounter++;
        if (Projectile.frameCounter > 2)
        {
            Projectile.frame++;
            Projectile.frameCounter = 3;
        }
        if (Projectile.frame >= 4)
        {
            Projectile.frame = 0;
        }
    }
}
