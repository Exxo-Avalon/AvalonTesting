using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged;

public class ArrowBeam : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pointing Laser");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 4 / 1;
        Projectile.height = dims.Height * 4 / 1 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.MaxUpdates = 100;
        Projectile.timeLeft = 100;
        Projectile.damage = 0;
        Projectile.penetrate = -1;
    }
    public override bool? CanCutTiles()
    {
        return false;
    }
    public override void AI()
    {
        Projectile.localAI[0]++;
        Player p = Main.player[Projectile.owner];
        if (Projectile.localAI[0] > 4f)
        {
            if ((Projectile.position.X > p.GetModPlayer<ExxoPlayer>().MousePosition.X && Projectile.position.X < p.position.X) ||
                (Projectile.position.X < p.GetModPlayer<ExxoPlayer>().MousePosition.X && Projectile.position.X > p.position.X))
            {
                for (var num617 = 0; num617 < 4; num617++)
                {
                    var value12 = Projectile.position;
                    value12 -= Projectile.velocity * num617 * 0.25f;
                    Projectile.alpha = 255;
                    var num618 = ModContent.DustType<Dusts.PointingDust>();
                    Color c = Color.White;
                    if (p.team == (int)Terraria.Enums.Team.Pink)
                    {
                        c = new Color(171, 59, 218);
                    }
                    else if (p.team == (int)Terraria.Enums.Team.Green)
                    {
                        c = new Color(59, 218, 85);
                    }
                    else if (p.team == (int)Terraria.Enums.Team.Blue)
                    {
                        c = new Color(59, 149, 218);
                    }
                    else if (p.team == (int)Terraria.Enums.Team.Yellow)
                    {
                        c = new Color(218, 183, 59);
                    }
                    else if (p.team == (int)Terraria.Enums.Team.Red)
                    {
                        c = new Color(218, 59, 59);
                    }
                    var num619 = Dust.NewDust(value12, 1, 1, num618, 0f, 0f, 0, default, 1f);
                    Main.dust[num619].position = value12;
                    //Main.dust[num619].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num619].color = c;
                    Main.dust[num619].velocity *= 0.2f;
                    Main.dust[num619].noGravity = true;
                }
            }
        }
    }
}
