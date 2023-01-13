using Avalon.Logic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using System.IO;

namespace Avalon.Projectiles.Melee;

public class ElementEarthBeam : ModProjectile
{
    Vector3 DiscoRGB;
    Color RGB;
    public float gravityTimer;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Earth Beam");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = 27;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = -1;
        Projectile.light = 0.3f;
        Projectile.friendly = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        if (Projectile.localAI[1] >= 15f)
        {
            return new Color(255, 255, 255, Projectile.alpha);
        }
        if (Projectile.localAI[1] < 5f)
        {
            return Color.Transparent;
        }
        int num7 = (int)((Projectile.localAI[1] - 5f) / 10f * 255f);
        return new Color(num7, num7, num7, num7);
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(gravityTimer);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        gravityTimer = reader.ReadSingle();
    }
    public override bool PreAI()
    {
        Lighting.AddLight(Projectile.position, 170f / 255f, 78f / 255f, 17f / 255f);
        return true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        return true;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {

    }
    public override void OnHitPvp(Player target, int damage, bool crit)
    {

    }
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        Player player = Main.player[Projectile.owner];
        Rectangle myRect = Projectile.Hitbox;
        if (Projectile.owner != Main.myPlayer)
        {
            return;
        }
        for (int npcIndex = 0; npcIndex < 200; npcIndex++)
        {
            NPC npc = Main.npc[npcIndex];
            if (!npc.active || npc.dontTakeDamage || ((!Projectile.friendly || (npc.friendly && (npc.type != NPCID.Guide || Projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || Projectile.owner >= 255 || !player.killClothier))) && (!Projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (Projectile.owner >= 0 && npc.immune[Projectile.owner] != 0 && Projectile.maxPenetrate != 1) || (!npc.noTileCollide && Projectile.ownerHitCheck))
            {
                continue;
            }
            bool stickingToNPC;
            if (npc.type == NPCID.SolarCrawltipedeTail)
            {
                Rectangle rect = npc.Hitbox;
                int num31 = 8;
                rect.X -= num31;
                rect.Y -= num31;
                rect.Width += num31 * 2;
                rect.Height += num31 * 2;
                stickingToNPC = Projectile.Colliding(myRect, rect);
            }
            else
            {
                stickingToNPC = Projectile.Colliding(myRect, npc.Hitbox);
            }
            if (!stickingToNPC)
            {
                continue;
            }
            if (npc.reflectsProjectiles && Projectile.CanBeReflected())
            {
                npc.ReflectProjectile(Projectile);
                break;
            }
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = npcIndex;
            Projectile.velocity = (npc.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            var array2 = (Point[])(object)new Point[10]; // 5 = maximum sticking in target
            int projCount = 0;
            for (int projIndex = 0; projIndex < 1000; projIndex++)
            {
                Projectile proj = Main.projectile[projIndex];
                if (projIndex != Projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == Projectile.type && proj.ai[0] == 1f && proj.ai[1] == (float)npcIndex)
                {
                    array2[projCount++] = new Point(projIndex, proj.timeLeft);
                    if (projCount >= array2.Length)
                    {
                        break;
                    }
                }
            }
            if (projCount < array2.Length)
            {
                continue;
            }
            int num30 = 0;
            for (int i = 1; i < array2.Length; i++)
            {
                if (array2[i].Y < array2[num30].Y)
                {
                    num30 = i;
                }
            }
            Main.projectile[array2[num30].X].Kill();
        }
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
        {
            targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
        }
        return null;
    }

    public override void AI()
    {
        int num12 = ModContent.DustType<Dusts.ElementEarthDust>();

        DiscoRGB = new Vector3(170f, 78f, 17f);
        RGB = new Color(DiscoRGB.X, DiscoRGB.Y, DiscoRGB.Z);

        if (Projectile.localAI[1] > 7f)
        {
            var num484 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 2f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 2f), 8, 8, num12, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, RGB, 1.25f);
            Main.dust[num484].velocity *= -0.25f;
            Main.dust[num484].noGravity = true;
            num484 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X * 2f + 2f, Projectile.position.Y + 2f - Projectile.velocity.Y * 2f), 8, 8, num12, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, RGB, 1.25f);
            Main.dust[num484].velocity *= -0.25f;
            Main.dust[num484].noGravity = true;
            Main.dust[num484].position -= Projectile.velocity * 0.5f;
        }

        if (gravityTimer == 0f)
        {
            Projectile.scale -= 0.02f;
            Projectile.alpha += 30;
            if (Projectile.alpha >= 250)
            {
                Projectile.alpha = 255;
                gravityTimer = 1f;
            }
        }
        else if (gravityTimer == 1f)
        {
            Projectile.scale += 0.02f;
            Projectile.alpha -= 30;
            if (Projectile.alpha <= 0)
            {
                Projectile.alpha = 0;
                gravityTimer = 0f;
            }
        }
        if (Projectile.ai[0] == 1f)
        {
            Vector2 center3 = Projectile.Center;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            int num930 = 5 * Projectile.MaxUpdates;
            bool flag39 = false;
            bool flag40 = false;
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] % 30f == 0f)
            {
                flag40 = true;
            }
            int num931 = (int)Projectile.ai[1];
            if (Projectile.localAI[0] >= 60 * num930)
            {
                flag39 = true;
            }
            else if (num931 < 0 || num931 >= 200)
            {
                flag39 = true;
            }
            else if (Main.npc[num931].active && !Main.npc[num931].dontTakeDamage)
            {
                Projectile.Center = Main.npc[num931].Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Main.npc[num931].gfxOffY;
                if (flag40)
                {
                    Main.npc[num931].HitEffect(0, 1.0);
                }
            }
            else
            {
                flag39 = true;
            }
            if (flag39)
            {
                Projectile.Kill();
            }
        }
        //Lighting.AddLight(Projectile.Center, 0.8f, 0.7f, 0.4f);
    }
    public override void Kill(int timeLeft)
    {
        DiscoRGB = new Vector3(Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
        RGB = new Color(DiscoRGB.X, DiscoRGB.Y, DiscoRGB.Z);

        SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        for (int num394 = 4; num394 < 24; num394++)
        {
            float num395 = Projectile.oldVelocity.X * (30f / num394);
            float num396 = Projectile.oldVelocity.Y * (30f / num394);
            int num12 = DustID.RainbowRod;
            int num398 = Dust.NewDust(new Vector2(Projectile.position.X - num395, Projectile.position.Y - num396), 8, 8, num12, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 100, RGB, 1.8f);
            Main.dust[num398].velocity *= 1f;
            Main.dust[num398].noGravity = true;
        }
    }
}
