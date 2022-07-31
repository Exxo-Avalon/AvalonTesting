using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class PyroBolt : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyro Bolt");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.Size = new Vector2(8);
        Projectile.aiStyle = 1;
        AIType = ProjectileID.WoodenArrowFriendly;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public SoundStyle Firework = new SoundStyle("Terraria/Sounds/Item_110")
    {
        Volume = 3f,
        PitchVariance = 0.2f,
        Pitch = -1f,
        MaxInstances = 10,
    };
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(Firework, Projectile.position);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (int i = 0; i < 16; i++)
        {
            Vector2 funnycircle = new Vector2(3).RotatedBy(i * (16 / MathHelper.Pi));
            int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, funnycircle.X, funnycircle.Y, 0, default, 1.5f);
            Main.dust[num10].noGravity = true;
        }
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PyroBoltExplosion>(), Projectile.damage, 0, Projectile.owner, 0, 100);
        return true;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (target.HasBuff(ModContent.BuffType<PyroBoltTier2>()))
        {
            for (int i = 0; i < 24; i++)
            {
                damage *= 2;
                Vector2 funnycircle = new Vector2(5).RotatedBy(i * (24 / MathHelper.Pi));
                int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, funnycircle.X, funnycircle.Y, 0, default, 2.5f);
                Main.dust[num10].noGravity = true;
            }
            target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 300);
            target.RequestBuffRemoval(ModContent.BuffType<PyroBoltTier2>());
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PyroBoltExplosion>(), Projectile.damage, 0, Projectile.owner, 1, 160);
        }
        else
        {
            if (target.HasBuff(ModContent.BuffType<PyroBoltTier1>()))
            {
                for (int i = 0; i < 24; i++)
                {
                    damage += (damage / 2);
                    Vector2 funnycircle = new Vector2(4).RotatedBy(i * (24 / MathHelper.Pi));
                    int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, funnycircle.X, funnycircle.Y, 0, default, 2f);
                    Main.dust[num10].noGravity = true;
                }
                target.AddBuff(ModContent.BuffType<PyroBoltTier2>(), 60);
                target.RequestBuffRemoval(ModContent.BuffType<PyroBoltTier1>());
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PyroBoltExplosion>(), Projectile.damage, 0, Projectile.owner, 0, 130);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    Vector2 funnycircle = new Vector2(3).RotatedBy(i * (16 / MathHelper.Pi));
                    int num10 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.InfernoFork, funnycircle.X, funnycircle.Y, 0, default, 1.5f);
                    Main.dust[num10].noGravity = true;
                }
                target.AddBuff(ModContent.BuffType<PyroBoltTier1>(), 120);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PyroBoltExplosion>(), Projectile.damage, 0, Projectile.owner, 0, 100);
            }
        }
    }
    public override void AI()
    {
        var dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 50, default, 2f);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].velocity *= 0.3f;
        if (Main.rand.NextBool(10))
        {
            int num161 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)),
                Main.rand.Next(61, 64));
            Gore gore30 = Main.gore[num161];
            Gore gore40 = gore30;
            gore40.velocity *= 0.3f;
            gore40.scale = Main.rand.NextFloat(0.5f, 1f);
            gore40.alpha = 100;
            Main.gore[num161].velocity.X += Main.rand.Next(-1, 2);
            Main.gore[num161].velocity.Y += Main.rand.Next(-1, 2);
        }
    }
}

public class PyroBoltTier1 : ModBuff
{
    public override string Texture => $"Terraria/Images/Buff_{BuffID.OnFire}";
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Marked");
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(6))
        {
            int num10 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.InfernoFork, 0f, -2f, 0, default, 1f);
            Main.dust[num10].noGravity = true;
        }
    }
}
public class PyroBoltTier2 : PyroBoltTier1
{
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (Main.rand.NextBool(3))
        {
            int num10 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.InfernoFork, 0f, -2f, 0, default, 1f);
            Main.dust[num10].noGravity = true;
        }
    }
}

public class PyroBoltExplosion : ModProjectile
{
    public override string Texture => $"Terraria/Images/Buff_{BuffID.OnFire}";
    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(0);
        Projectile.friendly = true;
        Projectile.aiStyle = 0;
        Projectile.penetrate = -1;
        Projectile.knockBack = 0;
        Projectile.timeLeft = 30;
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
        if (Projectile.ai[0] == 1)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Inferno>(), 120);
        }
    }
}
