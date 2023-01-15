using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Projectiles.Ranged;

public class DemonSpikeScale : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Demon Spikescale");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 8 / 16;
        Projectile.height = dims.Height * 8 / 16 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.MaxUpdates = 1;
        Projectile.scale = 1f;
        Projectile.timeLeft = 1200;
        Projectile.DamageType = DamageClass.Ranged;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        if (Projectile.ai[1] != 1)
            return true;
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Color col = Color.Lerp(Color.White, Color.Lavender, Main.masterColor);
        Vector2 stretchscale = new Vector2(Projectile.scale - (Vector2.Distance(Projectile.position, Projectile.oldPosition) * 0.01f), Projectile.scale + (Vector2.Distance(Projectile.position, Projectile.oldPosition) * 0.1f));


        for (int i = 1; i < Projectile.oldPos.Length; i++)
        {
            col.A = 0;
            Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + frameOrigin;
            Main.EntitySpriteDraw(texture, drawPos, frame, new Color(col.R / i, col.G / i, col.B / i, 0), Projectile.oldRot[i], frameOrigin, new Vector2(stretchscale.X + (i * 0.1f), stretchscale.Y + (i * 0.1f)), SpriteEffects.None, 0);
        }
        col.A = 150;
        Main.EntitySpriteDraw(texture, Projectile.position - Main.screenPosition + frameOrigin, frame, Color.Lerp(col, Color.White, 0.5f) * Projectile.Opacity, Projectile.rotation, frameOrigin, stretchscale * 1.1f, SpriteEffects.None, 0);

        return false;
    }
    public override void Kill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num133 = 0; num133 < 3; num133++)
        {
            float num134 = -Projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
            float num135 = -Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + num134, Projectile.position.Y + num135, num134, num135, ModContent.ProjectileType<SpikeShard>(), (int)(Projectile.damage * 0.33), 0f, Projectile.owner, 0f, 0f);
        }
    }
    public override void AI()
    {
        if (Projectile.ai[1] == 1)
        {
            Projectile.extraUpdates = 7;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
