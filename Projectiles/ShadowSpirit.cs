using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles;

public class ShadowSpirit : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Spirit");
    }
    public override void SetDefaults()
    {
        Projectile.penetrate = 1;
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 900;
    }
    public float maxSpeed = 12.5f;
    public float homeDistance = 400;
    public float homeStrength = 5f;
    public override void AI()
    {
        Vector2 startPosition = Projectile.Center;
        int closest = Projectile.FindClosestNPC(homeDistance, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly);
        if(closest != -1)
        {
            if (Collision.CanHit(Main.npc[closest], Projectile))
            {
                Vector2 target = Main.npc[closest].Center;
                float distance = Vector2.Distance(target, startPosition);
                Vector2 goTowards = Vector2.Normalize(target - startPosition) * ((homeDistance - distance) / (homeDistance / homeStrength));

                Projectile.velocity += goTowards;

                if (Projectile.velocity.Length() > maxSpeed)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * maxSpeed;
                }
            }
        }

        Projectile.rotation = Projectile.velocity.ToRotation();

        int dust = Dust.NewDust(Projectile.position, 16, 16, DustID.Ghost, 0f, 0f, 220, Color.Purple, 1.5f);
        Main.dust[dust].noLight = false;
        Main.dust[dust].velocity *= 0f;
        Main.dust[dust].noGravity = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
}
