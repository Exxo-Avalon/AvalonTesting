using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;

namespace Avalon.Projectiles.Melee;

public class SoulEdgeSlash : SwordSwingSwirly
{
    public override Color SparkleColor => new Color(10, 10, 10, 0);
    public override Color BigSparkleColor => new Color(3, 68, 116, 0);
    public override Color color1 => new Color(6, 101, 234);
    public override Color color2 => new Color(171, 237, 255);
    public override Color color3 => color1;
    public override float scalemod => 1f;
    public override bool CanCutTile => true;

    public override int Dust1 => DustID.FireworksRGB;
    public override Color Dust1Color => Color.Lerp(Color.CornflowerBlue, Color.Blue, Main.rand.NextFloat() * 1f);
    public override int Dust2 => 43;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.ownerHitCheck = true;
        Projectile.ownerHitCheckDistance = 300f;
        Projectile.scale = 2f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
        Projectile.alpha = 255;
    }
}

public class SoulEdgeSlash2 : SwordSwingGeneric
{
    public override Color SparkleColor => new Color(30, 30, 80, 0);
    public override Color BigSparkleColor => new Color(3, 68, 116, 0);
    public override Color color1 => new Color(6, 101, 234);
    public override Color color2 => new Color(171, 237, 255);
    public override Color color3 => color1;
    public override float scalemod => 0.8f;
    public override bool CanCutTile => true;

    public override int Dust1 => DustID.FireworksRGB;
    public override Color Dust1Color => Color.Lerp(Color.CornflowerBlue, Color.Blue, Main.rand.NextFloat() * 1f);
    public override int Dust2 => 43;
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.ownerHitCheck = true;
        Projectile.ownerHitCheckDistance = 300f;
        Projectile.scale = 4f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
        Projectile.alpha = 255;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        return false;
    }
}
