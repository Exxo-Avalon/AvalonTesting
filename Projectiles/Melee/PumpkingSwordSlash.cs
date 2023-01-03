using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;

namespace Avalon.Projectiles.Melee;

public class PumpkingSwordSlash : SwordSwingGeneric
{
    public override Color SparkleColor => new Color(64, 32, 25, 0);
    public override Color BigSparkleColor => new Color(128, 64, 25, 0);
    public override Color color1 => new Color(128, 42, 0);
    public override Color color2 => new Color(255, 200, 0);
    public override Color color3 => color1;
    public override float scalemod => 1.15f;
    public override bool CanCutTile => true;

    public override int Dust1 => DustID.Torch;
    public override Color Dust1Color => Color.Lerp(Color.Gold, Color.Red, Main.rand.NextFloat() * 1f);
    public override int Dust2 => DustID.Torch;
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
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        int debuffCount = 0;
        for (int i = 0; i < target.buffType.Length; i++)
        {
            if (Main.debuff[target.buffType[i]])
            {
                debuffCount++;
            }
        }
        if (debuffCount > 0)
        {
            if (target.boss)
            {
                damage = (int)(damage * 1.2 * debuffCount);
            }
            else
            {
                damage = (int)(damage * 1.45 * debuffCount);
            }
        }
    }
}
