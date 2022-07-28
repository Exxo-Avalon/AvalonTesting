using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class VirulentScythe : ModProjectile
{
    private byte counter;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Scythe");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.Size = new Vector2(32);
        Projectile.aiStyle = ProjAIStyleID.Boomerang;
        //AIType = ProjectileID.EnchantedBoomerang
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 0;
        Projectile.timeLeft = 2400;
        counter = 0;
    }

    public override void AI()
    {
        //Main.NewText(Projectile.velocity);

        if (Projectile.ai[0] != 0)
        {
            counter++;
            if (counter == 20)
            {
                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<VirulentExtraScythe>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Main.projectile[p].rotation = Projectile.rotation + 1.95f;
            }
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2(Projectile.width / 2 - frameOrigin.X, Projectile.height - frame.Height);
        Vector2 drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

        for (int i = 0; i < 7; i += 3)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * -i, Projectile.velocity.Y * -i), frame, new Color(0, 255 - 255 / 7 * i, 0, 100), Projectile.rotation, frameOrigin, Projectile.scale - 0.01f * i, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, Color.White, Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
