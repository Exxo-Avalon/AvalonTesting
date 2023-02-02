using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;
using System;

namespace Avalon.Projectiles.Melee;

public class blasfahbetter : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.ownerHitCheck = true;
        Projectile.ownerHitCheckDistance = 300f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
        Projectile.alpha = 255;
    }
    public override void AI()
    {
        Projectile.localAI[0]++;
        Player player = Main.player[Projectile.owner];
        float num = Projectile.localAI[0] / Projectile.ai[1];
        float num2 = Projectile.ai[0];
        float num3 = Projectile.velocity.ToRotation();
        float num4 = (Projectile.rotation = (float)Math.PI * num2 * num + num3 + num2 * (float)Math.PI + player.fullRotation);
        float num5 = 0.2f;
        float num6 = 1f;

        float num10 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector5 = Projectile.Center + num10.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector6 = (num10 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        Dust dust4 = Dust.NewDustPerfect(Projectile.Center + num10.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 54, vector6 * 1f, 100, Color.Lerp(Color.HotPink, Color.White, Main.rand.NextFloat() * 0.3f), 0.4f);
        dust4.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
        dust4.noGravity = true;
        Dust.NewDustPerfect(vector5, 43, vector6 * 1f, 100, Color.White, 1.2f);
        //Projectile.Center = player.Center;
    }
}
