using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;
using Avalon.Buffs;

namespace Avalon.Projectiles.Melee;

public class VorazylcumKatanaSlash : SwordSwingGeneric
{
    public override Color SparkleColor => new Color(99, 89, 99, 0);
    public override Color BigSparkleColor => new Color(200, 8, 120, 0);
    public override Color color1 => new Color(178, 0, 80);
    public override Color color2 => new Color(255, 115, 176);
    public override Color color3 => color1;
    public override float scalemod => 1f;
    public override bool CanCutTile => true;
    public override int Dust1 => DustID.FireworksRGB;
    public override Color Dust1Color => Color.Lerp(Color.Magenta, Color.Pink, Main.rand.NextFloat() * 1f);
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
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        target.AddBuff(ModContent.BuffType<KunziteSparks>(), 120);
        //target.GetGlobalNPC<KunziteEnergyGlobalNPC>().KunziteBladeCooldown = 0;
    }
}
