using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Ranged;

public class XanthophyteArrow : ModProjectile
{
    private byte bounceCounter;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Arrow");
    }

    public override void SetDefaults()
    {
        Projectile.arrow = true;
        Rectangle dims = this.GetDims();
        Projectile.penetrate = 4;
        Projectile.width = dims.Width * 10 / 32;
        Projectile.height = dims.Height * 10 / 32 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(bounceCounter);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        bounceCounter = reader.ReadByte();
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Projectile P = Projectile;
        P.ai[0]++;
        if (P.ai[0] >= 45)
        {
            for (int i = 0; i < 3; i++)
            {
                float vX = P.velocity.X + Main.rand.Next(-80, 81) * 0.05f;
                float vY = P.velocity.Y + Main.rand.Next(-80, 81) * 0.05f;
                int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X, P.position.Y, vX, vY, ModContent.ProjectileType<XanthophyteArrowSplit>(), P.damage, P.knockBack, P.owner);
            }
            P.ai[0] = 0;
            Projectile.active = false;
        }
        Projectile.ai[1]++;
        if (Projectile.ai[1] >= 15f)
        {
            Projectile.ai[1] = 15f;
            Projectile.velocity.Y += 0.1f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (bounceCounter < 3)
        {
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            bounceCounter++;
        }
        else
            Projectile.Kill();
        return false;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num121 = 0; num121 < 10; num121++)
        {
            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.ContagionSpray>(), 0f, 0f, 150, default, 1.2f);
            Main.dust[d].noGravity = true;
        }
    }
}
