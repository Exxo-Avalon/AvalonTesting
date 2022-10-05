using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;
using System;

namespace Avalon.Projectiles.Melee;
public abstract class VertexSlash : SwordSwingGeneric
{
    public bool CanCutTile { get; set; }
    public override void SetDefaults()
    {
        CanCutTile = true;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.ownerHitCheck = true;
        Projectile.ownerHitCheckDistance = 300f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 30;
    }
}
