using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.IO;

namespace Avalon.Projectiles.Ranged;

public class XanthophyteArrowSplit : ModProjectile
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
        Projectile.aiStyle = ProjAIStyleID.Arrow;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (bounceCounter < 3)
        {
            Projectile.velocity *= -1;
            bounceCounter++;
        }
        else
            Projectile.Kill();
        return false;
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(bounceCounter);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        bounceCounter = reader.ReadByte();
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
