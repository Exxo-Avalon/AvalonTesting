using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace AvalonTesting.Projectiles.Melee;

public class CaesiumSpear : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Pike");
    }
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = 19;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.1f;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Melee;
    }
    public float movementFactor
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        SoundEngine.PlaySound(SoundID.Item14, target.position);
        for (int i = 0; i < 5; i++)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, Main.rand.Next(-2, 3), Main.rand.Next(-2, 3), ModContent.ProjectileType<Projectiles.CaesiumExplosion>(), damage, 5f, Projectile.owner, 0f, 0f);
        }
        target.AddBuff(BuffID.OnFire, 60 * 5);
    }
    public override void AI()
    {
        Player projOwner = Main.player[Projectile.owner];
        Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
        Projectile.direction = projOwner.direction;
        projOwner.heldProj = Projectile.whoAmI;
        projOwner.itemTime = projOwner.itemAnimation;
        Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
        Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
        if (!projOwner.frozen)
        {
            if (movementFactor == 0f)
            {
                movementFactor = 3f;
                Projectile.netUpdate = true;
            }
            if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
            {
                movementFactor -= 2.1f;
            }
            else
            {
                movementFactor += 1.8f;
            }
        }
        Projectile.position += Projectile.velocity * movementFactor;
        if (projOwner.itemAnimation == 0)
        {
            Projectile.Kill();
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        if (Projectile.spriteDirection == -1)
        {
            Projectile.rotation -= MathHelper.ToRadians(90f);
        }
    }
}
