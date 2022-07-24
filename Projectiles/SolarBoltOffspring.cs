using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

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

        color = new Color(255, 50, 0) * 0.4f;
        dustId = 152;
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
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 0.75f, 0.4f, 0);
        for (var i = 0; i < 2; i++)
        {
            var dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.velocity.X, Projectile.velocity.Y, 50, color, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
        }
        if (Projectile.ai[1] == 0f)
        {
            Projectile.ai[1] = 1f;
            SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
        }

        Lighting.AddLight(new Vector2((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f)), color.ToVector3());
    }

    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        for (int num453 = 0; num453 < 15; num453++)
        {
            int num454 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustId, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 50, color, 1.2f);
            Main.dust[num454].noGravity = true;
            Dust dust152 = Main.dust[num454];
            Dust dust226 = dust152;
            dust226.scale *= 1.25f;
            dust152 = Main.dust[num454];
            dust226 = dust152;
            dust226.velocity *= 0.5f;
        }
    }
}
