using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace AvalonTesting.Projectiles.Melee;

public class QuantumBeam : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quantum Beam");
    }
    public override void SetDefaults()
    {
        Projectile.width = 15;
        Projectile.height = 15;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 600;
        Projectile.scale = 0.8f;
        Projectile.tileCollide = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        /*
        Color[] CoolColors = {
                new Color(255, 0, 255, 0),
                new Color(128, 0, 255, 0),
                new Color(128, 0, 128, 0),
                new Color(255, 0, 128, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 60f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;

        return Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade);
        */
        return Color.Black;
    }
    public override void AI()
    {
        Color[] CoolColors = {
                new Color(255, 0, 255, 0),
                new Color(128, 0, 255, 0),
                new Color(128, 0, 128, 0),
                new Color(255, 0, 128, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 25f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
        Vector2 PartingofthePurpleSea1 = new Vector2(3).RotatedBy(Projectile.rotation);
        Vector2 PartingofthePurpleSea2 = new Vector2(-3).RotatedBy(Projectile.rotation);

        int CoolDust1 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0,0, DustID.FireworksRGB, PartingofthePurpleSea1.X, PartingofthePurpleSea1.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
        Main.dust[CoolDust1].noGravity = true;
        int CoolDust2 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea2.X, PartingofthePurpleSea2.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
        Main.dust[CoolDust2].noGravity = true;
    }
    public override void Kill(int timeLeft)
    {

        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Color[] CoolColors = {
                new Color(255, 0, 255, 0),
                new Color(128, 0, 255, 0),
                new Color(128, 0, 128, 0),
                new Color(255, 0, 128, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 25f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;

        Color GlowColor = Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade);
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2((Projectile.width / 1) - frameOrigin.X, Projectile.height - frame.Height);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        for (int i = 0; i < 8; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos - (Projectile.velocity * (1.5f * (i + 1))), frame, new Color(GlowColor.R - i * 48, GlowColor.G - i * 48, GlowColor.B - i * 48, 0), Projectile.rotation, frameOrigin, Projectile.scale + (i * 0.2f), SpriteEffects.None, 0);
        }

        Main.EntitySpriteDraw(texture, drawPos, frame, Color.Black, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
        return false;
    }
}

public class QuantumBeam2 : QuantumBeam
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quantum Beam");
    }
    public override void SetDefaults()
    {
        Projectile.width = 15;
        Projectile.height = 15;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 200;
        Projectile.scale = 0.3f;
        Projectile.tileCollide = false;
    }
    public override void AI()
    {
        Color[] CoolColors = {
                new Color(255, 0, 128, 0),
                new Color(128, 0, 128, 0),
                new Color(128, 0, 255, 0),
                new Color(255, 0, 255, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 25f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
        Vector2 PartingofthePurpleSea1 = new Vector2(3).RotatedBy(Projectile.rotation);
        Vector2 PartingofthePurpleSea2 = new Vector2(-3).RotatedBy(Projectile.rotation);

        int CoolDust1 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea1.X, PartingofthePurpleSea1.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
        Main.dust[CoolDust1].noGravity = true;
        int CoolDust2 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea2.X, PartingofthePurpleSea2.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
        Main.dust[CoolDust2].noGravity = true;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Color[] CoolColors = {
                new Color(255, 0, 128, 0),
                new Color(128, 0, 128, 0),
                new Color(128, 0, 255, 0),
                new Color(255, 0, 255, 0),
            };
        int numColors = CoolColors.Length;
        float fade = (Main.GameUpdateCount % 25) / 25f;
        int index = (int)((Main.GameUpdateCount / 25) % numColors);
        int nextIndex = (index + 1) % numColors;

        Color GlowColor = Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade);
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2((Projectile.width / 1) - frameOrigin.X, Projectile.height - frame.Height);
        Vector2 drawPos = Projectile.Center - Main.screenPosition;

        for (int i = 0; i < 8; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos - (Projectile.velocity * (1.5f * (i + 1))), frame, new Color(GlowColor.R - i * 48, GlowColor.G - i * 48, GlowColor.B - i * 48, 0), Projectile.rotation, frameOrigin, Projectile.scale + (i * 0.2f), SpriteEffects.None, 0);
        }

        Main.EntitySpriteDraw(texture, drawPos, frame, Color.Black, Projectile.rotation, frameOrigin, new Vector2(Projectile.scale, Projectile.scale), SpriteEffects.None, 0);
        return false;
    }
}
