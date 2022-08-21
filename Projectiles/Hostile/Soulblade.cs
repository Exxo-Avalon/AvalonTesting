using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile;

public class Soulblade : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soulblade");
    }
    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 40;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.alpha = 0;
        Projectile.friendly = false;
        Projectile.hostile = true;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public override void AI()
    {
        Projectile.direction = Projectile.spriteDirection;
        Projectile.rotation -= 0.2f * Projectile.direction;
        Projectile.velocity *= 1.025f;
        if (Projectile.ai[0] == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 3f;
                Main.dust[num893].scale = 2f;
                Main.dust[num893].noGravity = true;
            }
            for (int i = 0; i < 20; i++)
            {
                int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num893].velocity *= 1f;
                Main.dust[num893].scale = 2f;
                Main.dust[num893].noGravity = true;
            }
            Projectile.ai[0] = 1;
        }
        if(Projectile.velocity.Length() > 20f)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 20f;
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin;

        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 2), Projectile.velocity.Y * (-i * 2)), frame, (new Color(255, 255, 255, 200) * (1 - (i * 0.25f))) * 0.5f, Projectile.rotation * (1 - (i * 0.1f)), frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, new Color(255, 255, 255, 200), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
