using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Magic;

public class SolarBoltOffspring : ModProjectile
{
    private Color color;
    private int dustId;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Solar Bolt Offspring");
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.SapphireBolt);
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 16;
        Projectile.height = dims.Height * 10 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = 3;
        Projectile.light = 0.1f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;

        color = new Color(255, 50, 0) * 0.4f;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        Projectile.ai[0]++;
        if (Projectile.ai[0] >= 5f)
        {
            Projectile.position += Projectile.velocity;
            Projectile.Kill();
        }
        else
        {
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
        }
        return false;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 120);
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 120);
    }
    public override void AI()
    {
        Projectile.velocity.Y += 0.1f;
        Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.Pi / 10);
        int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X, Projectile.velocity.Y, 50, color, 1f);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].velocity *= 0.3f;
        if (Main.rand.NextBool(10))
        {
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)),
                Main.rand.Next(61, 64));
            Gore gore40 = Main.gore[num161];
            gore40.velocity *= 0.3f;
            gore40.scale = Main.rand.NextFloat(0.5f, 1f);
            gore40.alpha = 150;
            Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
            Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
        }
    }
    public override bool? CanHitNPC(NPC target)
    {
        return Projectile.timeLeft <= 290;
    }
    public override void Kill(int timeLeft)
    {
        int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)),
                Main.rand.Next(61, 64));
        Gore gore40 = Main.gore[num161];
        gore40.velocity *= 0.3f;
        gore40.scale = Main.rand.NextFloat(0.5f, 1f);
        gore40.alpha = 150;
        Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
        Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
    }
}
