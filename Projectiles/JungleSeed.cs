using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Projectiles;

public class JungleSeed : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Jungle Seed");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 8 / 14;
        Projectile.height = dims.Height * 8 / 14 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        return true;
    }
    public override void AI()
    {
        if (Projectile.type == ModContent.ProjectileType<Boomlash>() || Projectile.type == ModContent.ProjectileType<VileSpit>())
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 15;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
        //if ((Projectile.type == ModContent.ProjectileType<MissileBolt>() && Projectile.ai[1] < 45f) || (Projectile.type != ModContent.ProjectileType<VileSpit>() && Projectile.type != ModContent.ProjectileType<RottenBullet>() && Projectile.type != ModContent.ProjectileType<PatellaBullet>() && Projectile.type != ModContent.ProjectileType<Soundwave>() && Projectile.type != ModContent.ProjectileType<FeroziumBullet>() && Projectile.type != ModContent.ProjectileType<Electrobullet>() && Projectile.type != ModContent.ProjectileType<SpikeCannon>() && Projectile.type != ModContent.ProjectileType<PathogenBullet>() && Projectile.type != ModContent.ProjectileType<MagmaticBullet>() && Projectile.type != ModContent.ProjectileType<TritonBullet>() && Projectile.type != ModContent.ProjectileType<FocusBeam>() && Projectile.type != ModContent.ProjectileType<VileSpit>() && Projectile.type != ModContent.ProjectileType<InfectiousSpore>()))
        //{
        //    Projectile.ai[0] += 1f;
        //}
        if (Projectile.type == ModContent.ProjectileType<VileSpit>())
        {
            for (var j = 0; j < 2; j++)
            {
                var num19 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height, DustID.CorruptGibs, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 80, default(Color), 1.3f);
                Main.dust[num19].velocity *= 0.3f;
                Main.dust[num19].noGravity = true;
            }
        }
        if (Projectile.type == ModContent.ProjectileType<InfectiousSpore>())
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
            }
        }
        if (Projectile.type == ModContent.ProjectileType<KunzitePulseBolt>())
        {
            if (Projectile.alpha < 170)
            {
                for (var n = 0; n < 10; n++)
                {
                    var x = Projectile.position.X - Projectile.velocity.X / 10f * n;
                    var y = Projectile.position.Y - Projectile.velocity.Y / 10f * n;
                    var num25 = Dust.NewDust(new Vector2(x, y), 1, 1, DustID.UnusedWhiteBluePurple, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num25].alpha = Projectile.alpha;
                    Main.dust[num25].position.X = x;
                    Main.dust[num25].position.Y = y;
                    Main.dust[num25].velocity *= 0f;
                    Main.dust[num25].noGravity = true;
                }
            }
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 25;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }
        else
        {
            if (Projectile.ai[0] >= 15f)
            {
                Projectile.ai[0] = 15f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
        }
        if (Projectile.type == ModContent.ProjectileType<Soundwave>())
        {
            Projectile.scale = Math.Min(11f, 185.08197f * (float)Math.Pow(0.99111479520797729, Projectile.timeLeft));
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            var v = Projectile.Center - new Vector2(Projectile.width * Projectile.scale / 2f, Projectile.height * Projectile.scale / 2f);
            var wH = new Vector2(Projectile.width * Projectile.scale, Projectile.height * Projectile.scale);
            var value2 = ClassExtensions.NewRectVector2(v, wH);
            var npc = Main.npc;
            for (var num57 = 0; num57 < npc.Length; num57++)
            {
                var nPC = npc[num57];
                if (nPC.active && !nPC.dontTakeDamage && !nPC.friendly && nPC.life >= 1 && nPC.getRect().Intersects(value2))
                {
                    nPC.StrikeNPC(Projectile.damage, Projectile.knockBack, (nPC.Center.X < Projectile.Center.X) ? -1 : 1, false, false);
                }
            }
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
