using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class Lunarang : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lunarang");
    }
    public override void SetDefaults()
    {
        //Rectangle dims = this.GetDims();
        Projectile.Size = new Vector2(22, 30);
        Projectile.aiStyle = 3;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = true;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 9000;
        Projectile.extraUpdates = 1;
        //DrawOffsetX = -9;
        //DrawOriginOffsetY = -9;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, Projectile.alpha);
    }
    public override void AI()
    {
        if (Main.rand.NextBool(3))
        {
            int dust = Dust.NewDust(Projectile.position, 22, 30, DustID.Enchanted_Pink, 0f, 0f, 200, default, 1.25f);
            Main.dust[dust].velocity *= 0.2f;
        }
    }
}
