using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Melee;

public class NickelShortsword : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nickel Shortsword");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.penetrate = -1;
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = ProjAIStyleID.ShortSword;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.hide = true;
        Projectile.timeLeft = 360;
    }
}
