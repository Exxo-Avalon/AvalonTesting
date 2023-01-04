using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Projectiles.Templates;
using System;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class PumpkingSwordSlash : SwordSwingGeneric
{
    public override Color SparkleColor => new Color(64, 32, 0, 0);
    public override Color BigSparkleColor => new Color(128, 64, 25, 0);
    public override Color color1 => new Color(100, 25, 0);
    public override Color color2 => new Color(255, 128, 0);
    public override Color color3 => color1;
    public override float scalemod => 1.3f;
    public override bool CanCutTile => true;

    public override int Dust1 => DustID.Torch;
    public override Color Dust1Color => Color.Lerp(Color.Yellow, Color.Red, Main.rand.NextFloat() * 1f);
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
    public SoundStyle ExplodyOverlapEdition = new SoundStyle("Terraria/Sounds/Item_14")
    {
        Volume = 0.6f,
        PitchVariance = 0.2f,
        MaxInstances = 15,
    };
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        //SoundEngine.PlaySound(ExplodyOverlapEdition, Projectile.Center);
        pumpkinSword(target.whoAmI, (int)(damage * 0.33f), knockback, Main.player[Projectile.owner]);
        for (int j = 0; j <= 20; j++)
        {
            int d = Dust.NewDust(target.Center, 0, 0, DustID.Torch, 0,0);
            Main.dust[d].noGravity = true;
            Main.dust[d].scale = 1.5f;
            Main.dust[d].fadeIn = Main.rand.NextFloat(1,2);
            Main.dust[d].velocity = Main.rand.NextVector2Circular(5, 5);
        }
    }
    private void pumpkinSword(int i, int dmg, float kb, Player p)
    {
        for (float o = 1; o <= 1.2f; o += 0.2f)
        {
            int logicCheckScreenHeight = (int)(Main.LogicCheckScreenHeight * o);
            int logicCheckScreenWidth = (int)(Main.LogicCheckScreenWidth * o);
            int num = Main.rand.Next(100, 300);
            int num2 = Main.rand.Next(100, 300);
            num = ((Main.rand.Next(2) != 0) ? (num + (logicCheckScreenWidth / 2 - num)) : (num - (logicCheckScreenWidth / 2 + num)));
            num2 = ((Main.rand.Next(2) != 0) ? (num2 + (logicCheckScreenHeight / 2 - num2)) : (num2 - (logicCheckScreenHeight / 2 + num2)));
            num += (int)p.position.X;
            num2 += (int)p.position.Y;
            Vector2 vector = new Vector2(num, num2);
            float num3 = Main.npc[i].position.X - vector.X;
            float num4 = Main.npc[i].position.Y - vector.Y;
            float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
            num5 = 8f / num5;
            num3 *= num5;
            num4 *= num5;
            int pumpkin = Projectile.NewProjectile(p.GetSource_ItemUse(Main.player[Projectile.owner].HeldItem), num, num2, num4 * 2, num5 * 2, ModContent.ProjectileType<PumpkinHead>(), dmg, kb, p.whoAmI, (float)i, 0f);
            Main.projectile[pumpkin].extraUpdates = Main.rand.Next(1, 3);
        }
    }
}
