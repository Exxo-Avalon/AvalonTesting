using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Magic;

public class SolarBolt : ModProjectile
{
    private Color color;
    private int dustId;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Solar Bolt");
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.SapphireBolt);
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 10 / 16;
        Projectile.height = dims.Height * 10 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.penetrate = 6;
        Projectile.light = 0.2f;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 1f, 0.55f, 0);
        for (var i = 0; i < 2; i++)
        {
            var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.InfernoFork, Projectile.velocity.X, Projectile.velocity.Y, 50, color, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
        }
        if (Main.rand.NextBool(10))
        {
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1f,1f), Main.rand.NextFloat(-1f, 1f)),
                Main.rand.Next(61, 64));
            Gore gore30 = Main.gore[num161];
            Gore gore40 = gore30;
            gore40.velocity *= 0.3f;
            gore40.scale = Main.rand.NextFloat(0.5f,1f);
            gore40.alpha = 100;
            Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
            Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
        }
        if (Projectile.ai[1] == 0f)
        {
            Projectile.ai[1] = 1f;
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
        }

        Lighting.AddLight(new Vector2((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f)), color.ToVector3());
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (Main.rand.NextBool(Projectile.penetrate + 1))
        {
            target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 120);
        }
        if(Projectile.penetrate == 2)
        {
            Projectile.damage *= 2;
        }
    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        if (Main.rand.NextBool(Projectile.penetrate + 1))
        {
            target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 120);
        }
        if (Projectile.penetrate == 2)
        {
            Projectile.damage *= 2;
        }
    }
    public override void Kill(int timeLeft)
    {
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SolarBoltExplosion>(), Projectile.damage, 0, Projectile.owner, 0, 100);
        SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
        for (int num453 = 0; num453 < 50; num453++)
        {
            int num454 = Dust.NewDust(Projectile.Center - new Vector2(25), Projectile.width + 50, Projectile.height + 50, DustID.InfernoFork, Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10), 50, color, 2.2f);
            Main.dust[num454].noGravity = true;
            Dust dust152 = Main.dust[num454];
            Dust dust226 = dust152;
            dust226.scale *= 1.25f;
            dust152 = Main.dust[num454];
            dust226 = dust152;
            dust226.velocity *= 0.5f;
        }
        for(int i = 0; i < 20; i++)
        {
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(10f, 10f)).RotatedBy(Projectile.velocity.ToRotation() + MathHelper.PiOver2),
                Main.rand.Next(61, 64));
            Gore gore30 = Main.gore[num161];
            Gore gore40 = gore30;
            gore40.velocity *= 0.3f;
            gore40.scale = Main.rand.NextFloat(0.5f, 1f);
            Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
            Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
        }
        for (int p = 0; p < Projectile.penetrate + Main.rand.Next(2); p++)
        {
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X + (Main.rand.Next(-40, 41) * 0.1f), Projectile.velocity.Y + (Main.rand.Next(-40, 41) * 0.1f), ModContent.ProjectileType<SolarBoltOffspring>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner);
            Main.projectile[proj].timeLeft = Main.rand.Next(250,350);
        }
    }
}
public class SolarBoltExplosion : ModProjectile
{
    public override string Texture => $"Terraria/Images/Buff_{BuffID.OnFire}";
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(0);
        Projectile.friendly = true;
        Projectile.aiStyle = 0;
        Projectile.penetrate = -1;
        Projectile.knockBack = 0;
        Projectile.timeLeft = 10;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 60;
        Projectile.tileCollide = false;
    }

    public override void OnSpawn(IEntitySource source)
    {
        Projectile.position -= new Vector2(Projectile.ai[1] / 2);
        Projectile.Size = new Vector2(Projectile.ai[1]);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        return false;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
            target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 60);
    }
}
