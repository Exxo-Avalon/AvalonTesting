using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class InfectedMist : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Infected Mist");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 30 / 16;
        Projectile.height = dims.Height * 30 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 6;
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 20 / 255f, 30 / 255f, 0);
        if (Projectile.type == ModContent.ProjectileType<InfectedMist>())
        {
            Projectile.velocity *= 0.96f;
            Projectile.alpha += 3;
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
        }
        else if (Projectile.type == ProjectileID.SporeCloud)
        {
            Projectile.velocity *= 0.96f;
            Projectile.alpha += 4;
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
        }
        else if (Projectile.type == ProjectileID.ChlorophyteOrb)
        {
            if (Projectile.ai[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 20f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                Projectile.velocity.X = Projectile.velocity.X * 0.98f;
            }
        }
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 5)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame >= Main.projFrames[Projectile.type])
        {
            Projectile.frame = 0;
        }
    }
}
