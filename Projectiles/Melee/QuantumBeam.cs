using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Avalon.Projectiles.Melee;

public class QuantumBeam : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quantum Beam");
    }
    public override void SetDefaults()
    {
        Projectile.width = 25;
        Projectile.height = 25;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.timeLeft = 300;
        Projectile.scale = 0.8f;
        Projectile.tileCollide = true;
        Projectile.penetrate = 5;
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
        if (Main.rand.NextBool(2))
        {
            int CoolDust1 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea1.X, PartingofthePurpleSea1.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
            Main.dust[CoolDust1].noGravity = true;
            int CoolDust2 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea2.X, PartingofthePurpleSea2.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
            Main.dust[CoolDust2].noGravity = true;
        }
    }

    public SoundStyle StarSoundReal = new SoundStyle("Terraria/Sounds/Item_9")
    {
        Volume = 0.6f,
        Pitch = -1f,
        PitchVariance = 1f,
        MaxInstances = 10,
    };

    public SoundStyle Impac = new SoundStyle("Terraria/Sounds/Item_72")
    {
        Volume = 0.6f,
        MaxInstances = 10,
    };
    public override void OnSpawn(IEntitySource source)
    {
        for (int i = 0; i <= 10; i++)
        {
            int FunnyDeathDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworksRGB, new Vector2(1, 1).RotatedBy(i * 36).X, new Vector2(1, 1).RotatedBy(i * 36).Y, 0, new Color(128, 0, 64, 0), 1);
            //Main.dust[FunnyDeathDust].noGravity = true;
        }
        SoundEngine.PlaySound(StarSoundReal, Projectile.position);
    }
    public override void Kill(int timeLeft)
    {
        for (int i = 0; i <= 20; i++)
        {
            int FunnyDeathDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworksRGB, new Vector2(2).RotatedBy(i * 18).X, new Vector2(2).RotatedBy(i * 18).Y, 0, new Color(64,0,128,0), 1);
            //Main.dust[FunnyDeathDust].noGravity = true;
        }
        SoundEngine.PlaySound(Impac, Projectile.position);
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        for (int i = 0; i <= 20; i++)
        {
            int FunnyDeathDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworksRGB, new Vector2(2).RotatedBy(i * 18).X, new Vector2(2).RotatedBy(i * 18).Y, 0, new Color(64, 0, 128, 0), 1);
            //Main.dust[FunnyDeathDust].noGravity = true;
        }
        SoundEngine.PlaySound(Impac, Projectile.position);
        target.AddBuff(BuffID.ShadowFlame, 300);
    }

    public override void OnHitPvp(Player target, int damage, bool crit)
    {
        for (int i = 0; i <= 20; i++)
        {
            int FunnyDeathDust = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworksRGB, new Vector2(2).RotatedBy(i * 18).X, new Vector2(2).RotatedBy(i * 18).Y, 0, new Color(64, 0, 128, 0), 1);
            //Main.dust[FunnyDeathDust].noGravity = true;
        }
        SoundEngine.PlaySound(Impac, Projectile.position);
        target.AddBuff(BuffID.ShadowFlame, 300);
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
        if (Main.rand.NextBool(10))
        {
            int CoolDust1 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea1.X, PartingofthePurpleSea1.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
            Main.dust[CoolDust1].noGravity = true;
            int CoolDust2 = Dust.NewDust(Projectile.Center + new Vector2(27, -27).RotatedBy(Projectile.rotation), 0, 0, DustID.FireworksRGB, PartingofthePurpleSea2.X, PartingofthePurpleSea2.Y, 0, Color.Lerp(CoolColors[index], CoolColors[nextIndex], fade), 1);
            Main.dust[CoolDust2].noGravity = true;
        }
    }
    public override void SetDefaults()
    {
        Projectile.width = 25;
        Projectile.height = 25;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.timeLeft = 100;
        Projectile.scale = 0.3f;
        Projectile.tileCollide = true;
        Projectile.localNPCHitCooldown = 1;
        Projectile.tileCollide = false;
    }
}
