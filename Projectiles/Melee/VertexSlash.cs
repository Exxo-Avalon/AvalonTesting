using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;
using System;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Avalon.Projectiles.Melee;
public class VertexSlash : SwordSwingGeneric
{
    public override Color color1 => new Color(139, 42, 156);
    public override Color color2 => new Color(236, 200, 19);
    public override Color color3 => color1;
    public override float scalemod => 1f;
    public override bool CanCutTile => true;
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
        bool hasDebuff = false;
        for (int i = 0; i < target.buffType.Length; i++)
        {
            if (Main.debuff[target.buffType[i]])
            {
                hasDebuff = true;
                break;
            }
        }
        if (hasDebuff)
        {
            if (target.boss)
                damage = (int)(damage * 1.3);
            else
                damage = (int)(damage * 1.6);
        }
    }
}
