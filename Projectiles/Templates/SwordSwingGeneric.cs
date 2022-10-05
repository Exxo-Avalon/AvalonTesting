using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Templates;
public abstract class SwordSwingGeneric : ModProjectile
{
    public abstract class VertexSlash : SwordSwingGeneric
    {
        public bool CanCutTile { get; set; }
        public override void SetDefaults()
        {
            CanCutTile = true;
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
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
            Player player = Main.player[Projectile.owner];
            float num = Projectile.localAI[0] / Projectile.ai[1];
            float num2 = Projectile.ai[0];
            float num3 = Projectile.velocity.ToRotation();
            float num4 = (Projectile.rotation = (float)Math.PI * num2 * num + num3 + num2 * (float)Math.PI + player.fullRotation);
            float num5 = 0.2f;
            float num6 = 1f;

            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) - Projectile.velocity;
            Projectile.scale = num6 + num * num5;

            float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
            Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
            if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
            {
                Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, vector3 * 1f, 100, Color.Lerp(Color.Gold, Color.White, Main.rand.NextFloat() * 0.3f), 0.4f);
                dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
                dust2.noGravity = true;
            }
            if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
            {
                Dust.NewDustPerfect(vector2, 43, vector3 * 1f, 100, Color.White * Projectile.Opacity, 1.2f * Projectile.Opacity);
            }
        }
        public override bool? CanCutTiles() => CanCutTile;
        /*
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float coneLength2 = 90f * Projectile.scale;
            float num3 = 0.5105088f * Projectile.ai[0];
            float maximumAngle2 = (float)Math.PI / 8f;
            float coneRotation2 = Projectile.rotation + num3;
            float num4 = (float)Math.PI / 4f * Projectile.ai[0];
            _ = Projectile.rotation;
            return targetHitbox.IntersectsCone(Projectile.Center, coneLength2, coneRotation2, maximumAngle2);
        }
        */
    }
}
