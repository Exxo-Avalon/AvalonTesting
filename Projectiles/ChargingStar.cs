using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.DataStructures;
using Avalon.Items.Material;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Avalon.UI;
using Avalon.Items.Weapons.Magic;

namespace Avalon.Projectiles;

class ChargingStar : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 84;
        Projectile.height = 84;
        Projectile.alpha = 0;
        Projectile.scale = 1;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.damage = 0;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Magic;
        DrawOriginOffsetY = 24;
        Projectile.extraUpdates = 0;
    }
    float notifySparkleSize;
    public override bool? CanHitNPC(NPC target) => false;
    public override bool CanHitPvp(Player target) => false;
    public override void AI()
    {
        Vector2 offset = new Vector2(40 * Projectile.spriteDirection, 0);
        Player player = Main.player[Projectile.owner];
        if (player.channel)
        {
            if (player.CheckMana(1, false))
            {
                if (Projectile.ai[0] / 3 == (int)(Projectile.ai[0] / 3) && Projectile.ai[1] > 0)
                player.CheckMana(1, true);
            }
            else
            {
                player.channel = false;
            }

            if (Projectile.ai[0] < 385)
            Projectile.ai[0]++;

            if (Projectile.ai[0] is 51 or 102 or 153 or 204 or 255)
            {
                //CombatText.NewText(Projectile.Hitbox, Color.Red, (int)Projectile.ai[1]);
                //if (player.CheckMana(player.GetManaCost(player.HeldItem), false))
                //{
                //player.CheckMana(player.GetManaCost(player.HeldItem), true);
                SoundEngine.PlaySound(SoundID.MaxMana);
                notifySparkleSize = 1f;
                Projectile.ai[1]++;
                //}
                //else
                //{
                //    player.channel = false;
                //}
            }
            if (Projectile.ai[0] == 384)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Charging"));
                player.channel = false;
                Projectile.ai[1] = 200;
                Projectile.ai[0] = 2200;
            }
        }
        else
        {
            Projectile.extraUpdates = 1;

            if (Projectile.ai[1] == 1)
                Projectile.ai[0] = 51;

            if (Projectile.ai[1] == 2)
                Projectile.ai[0] = 102;

            if (Projectile.ai[1] == 3)
                Projectile.ai[0] = 153;

            if (Projectile.ai[1] == 4)
                Projectile.ai[0] = 204;

            if (Projectile.ai[1] == 5)
                Projectile.ai[0] = 255;

            Projectile.ai[1] = -1;

            //regular shots
            if (Projectile.ai[0] is 51 or 102 or 153 or 204 or 255)
            {
                //CombatText.NewText(Projectile.Hitbox, Color.Red, (int)Projectile.ai[0]);
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), player.position);
                if(Main.myPlayer == player.whoAmI)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + offset.RotatedBy(Projectile.rotation),new Vector2(8 * Projectile.spriteDirection,0).RotatedBy(Projectile.rotation),ModContent.ProjectileType<TheSun>(),Projectile.damage,Projectile.knockBack,player.whoAmI);
                }
            }
            Projectile.ai[0]--;

            //Overcharge Shot
            if (Projectile.ai[0] == 1950)
            {
                SoundEngine.PlaySound(SoundID.Item14, player.position);
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), player.position);
                if (Main.myPlayer == player.whoAmI)
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI,Projectile.whoAmI), 200, Projectile.spriteDirection, false);
                    for (int i = 0; i < 8; i++)
                    {
                        int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + offset.RotatedBy(Projectile.rotation), new Vector2(Main.rand.NextFloat(8, 12) * Projectile.spriteDirection, 0).RotatedBy(Projectile.rotation).RotatedByRandom(MathHelper.Pi / 8), ModContent.ProjectileType<TheSun>(), (int)(Projectile.damage * 1.5f), Projectile.knockBack, player.whoAmI);
                        Main.projectile[proj].scale *= Main.rand.NextFloat(0.5f, 1.2f);

                        player.velocity -= new Vector2(2 * player.direction, 0).RotatedBy(Projectile.rotation);
                    }
                }
            }
            if (Projectile.ai[0] == 1900)
            {
                Projectile.active = false;
            }
        }
        if (Projectile.ai[0] > 0)
        {
            Projectile.timeLeft = 12;
        }

        #region visuals which ever so slightly interfere with other things
        if (player.channel || Projectile.ai[0] > 1950)
        {
            //dust
            Vector2 pos = (Projectile.Center + offset.RotatedBy(Projectile.rotation)) + Main.rand.NextVector2Circular(60, 60);
            Vector2 direction = pos.DirectionTo((Projectile.Center + offset.RotatedBy(Projectile.rotation)));
            int d = Dust.NewDust(pos, 0, 0, DustID.RainbowRod);
            Main.dust[d].noLightEmittence = true;
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity = direction * Main.dust[d].position.Distance(pos) * 0.6f + player.velocity;
            //Main.dust[d].fadeIn = MathHelper.Clamp(Projectile.ai[0],0,30) / 200;
            Main.dust[d].scale = MathHelper.Clamp(Projectile.ai[0], 0, 255) / 255;
            Main.dust[d].color = new Color(255, 255, 255, 0) * (MathHelper.Clamp(Projectile.ai[0], 0, 255) / 256);
            if (Projectile.ai[0] > 256)
            {
                Main.dust[d].color.G -= (byte)(Projectile.ai[0] - 255);
                Main.dust[d].color.B -= (byte)(Projectile.ai[0] - 255);
            }
        }
        //visuals basically
        player.heldProj = Projectile.whoAmI;
        player.direction = (Main.MouseWorld.X > player.Center.X) ? 1 : -1;
        Projectile.spriteDirection = (player.direction);
        Projectile.rotation = player.Center.DirectionTo(Main.MouseWorld).ToRotation();
        Projectile.rotation += (player.direction < 0) ? MathHelper.Pi : 0;
        //Projectile.Center = player.Center + new Vector2(30 * player.direction, -4).RotatedBy(Projectile.rotation) + (Main.rand.NextVector2Circular(Projectile.ai[0], Projectile.ai[0]) * 0.01f);
        Projectile.Center = player.Center + new Vector2(30 * player.direction, -4).RotatedBy(Projectile.rotation);
        player.itemTime = Projectile.timeLeft;
        player.SetDummyItemTime(Projectile.timeLeft);
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2 + MathHelper.PiOver2 * (player.direction - 1));
        #endregion visuals which ever so slightly interfere with other things
    }
    float starspin;
    public override bool PreDraw(ref Color lightColor)
    {
        if (notifySparkleSize > 0)
        {
            notifySparkleSize -= 0.05f;
        }
        Vector2 offset = new Vector2(40 * Projectile.spriteDirection, 0);
        Vector2 offset2 = new Vector2(-14 * Projectile.spriteDirection, -10);
        Player player = Main.player[Projectile.owner];
        SpriteEffects flip = (Projectile.spriteDirection < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
        Rectangle frame = texture.Frame();
        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 drawPos = Projectile.position + frameOrigin - Main.screenPosition + new Vector2(0,24);
        Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>(Texture + "WhiteGlow");
        Rectangle frame2 = texture2.Frame();
        Vector2 frameOrigin2 = frame2.Size() / 2f;
        Vector2 drawPos2 = Projectile.position + frameOrigin2 - Main.screenPosition + new Vector2(0, 24);
        Texture2D texture3 = (Texture2D)ModContent.Request<Texture2D>(Texture + "WhiteGradient");
        Rectangle frame3 = texture3.Frame();
        Vector2 frameOrigin3 = frame3.Size() / 2f;
        Vector2 drawPos3 = Projectile.position + frameOrigin3 - Main.screenPosition + new Vector2(0, 24);

        Texture2D glowy = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklyBig").Value;
        Rectangle frameG = glowy.Frame();
        Vector2 frameOriginG = frameG.Size() / 2f;
        Vector2 drawPosG = (Projectile.Center + offset.RotatedBy(Projectile.rotation)) - Main.screenPosition;
        Vector2 drawPosG2 = (Projectile.Center + offset2.RotatedBy(Projectile.rotation)) - Main.screenPosition;

        Vector2 random = (player.channel) ? (Main.rand.NextVector2Circular(Projectile.ai[0], Projectile.ai[0]) * 0.01f) : Vector2.Zero;
        random = (Projectile.ai[0] is > 512 and < 1940 or > 1950) ? (Main.rand.NextVector2Circular(8,8)) : random;

        //Color RealColorHours = (Projectile.ai[0] < 666) ? Color.Lerp(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 256), new Color(0, 64, 256, 256), 0.5f) : Color.Red;
        Color RealColorHours = (Projectile.ai[0] < 666) ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 256) : Color.Red;
        Main.EntitySpriteDraw(texture, drawPos + random, frame, Color.Lerp(lightColor,Color.White,0.3f), Projectile.rotation, frameOrigin, 1, flip, 0);;
        Main.EntitySpriteDraw(texture2, drawPos2 + random, frame2, RealColorHours, Projectile.rotation, frameOrigin2, 1, flip, 0);
        Main.EntitySpriteDraw(texture2, drawPos2 + random, frame2, Color.Lerp(new Color(256, 256, 256 ,0),new Color(0,0,0,0),Main.masterColor), Projectile.rotation, frameOrigin2, 1, flip, 0);
        RealColorHours.A = 0;
        Main.EntitySpriteDraw(texture3, drawPos3 + random, frame3, new Color(RealColorHours.R,RealColorHours.G, RealColorHours.B, 0) * (Projectile.ai[0] / 256), Projectile.rotation, frameOrigin3, 1, flip, 0);
        starspin += 0.1f;
        for (int i = 0; i < 2; i++)
        {

            Main.EntitySpriteDraw(glowy, drawPosG + random, frameG, new Color(RealColorHours.R, RealColorHours.G, RealColorHours.B, 0) * (Projectile.ai[0] / 512), Projectile.rotation + starspin + (MathHelper.PiOver2 * i) + MathHelper.PiOver4, frameOriginG, new Vector2(MathHelper.Clamp(Projectile.ai[0], 0, 512) * MathHelper.Lerp(0.003f, 0.005f, Main.masterColor), MathHelper.Clamp(Projectile.ai[0], 0, 512) * MathHelper.Lerp(0.005f, 0.0013f, Main.masterColor)), SpriteEffects.None, 0);
            Main.EntitySpriteDraw(glowy, drawPosG + random, frameG, new Color(255,255,255,0), Projectile.rotation + starspin + (MathHelper.PiOver2 * i), frameOriginG, new Vector2(MathHelper.Clamp(Projectile.ai[0],0,512) * MathHelper.Lerp(0.002f,0.004f,Main.masterColor), MathHelper.Clamp(Projectile.ai[0], 0, 512) * MathHelper.Lerp(0.004f, 0.0012f, Main.masterColor)), SpriteEffects.None, 0);

        }

        for (int i = 0; i < 2; i++)
        {

            Main.EntitySpriteDraw(glowy, drawPosG2 + random, frameG, new Color(RealColorHours.R, RealColorHours.G, RealColorHours.B, 0) * (Projectile.ai[0] / 512), Projectile.rotation + starspin + (MathHelper.PiOver2 * i) + MathHelper.PiOver4, frameOriginG, notifySparkleSize, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(glowy, drawPosG2 + random, frameG, new Color(255, 255, 255, 0), Projectile.rotation + starspin + (MathHelper.PiOver2 * i), frameOriginG, notifySparkleSize, SpriteEffects.None, 0);

        }

        return false;
    }
    public override bool ShouldUpdatePosition() => false;

    #region old
    //float CHARGE = 0;
    //int STUFF = 0;
    //int Pindex = -1;
    //Color Color1 = Color.Yellow;
    //Color Color2 = Color.Orange;
    //public override void SetStaticDefaults()
    //{
    //    DisplayName.SetDefault("Charging Star");
    //}

    //public override void SetDefaults()
    //{
    //    Projectile.width = 10;
    //    Projectile.height = 10;
    //    Projectile.alpha = 0;
    //    Projectile.scale = 1;
    //    Projectile.aiStyle = -1;
    //    Projectile.timeLeft = 240;
    //    Projectile.friendly = true;
    //    Projectile.penetrate = -1;
    //    Projectile.damage = 0;
    //    Projectile.hide = true;
    //    Projectile.ownerHitCheck = true;
    //    Projectile.tileCollide = false;
    //    Projectile.DamageType = DamageClass.Magic;
    //}
    //public override void Kill(int timeLeft)
    //{
    //    Projectile.active = false;
    //}
    //public override void AI()
    //{
    //    Lighting.AddLight(Projectile.position, 255 / 255f, 224 / 255f, 115 / 255f);
    //    CHARGE++;
    //    Projectile P = Projectile;
    //    P.damage = 0;
    //    Player O = Main.player[P.owner];
    //    if (CHARGE == 149)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE == 169)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE == 189)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE == 209)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE == 229)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE == 249)
    //    {
    //        Pindex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), P.position.X + P.width / 2, P.position.Y + P.height / 2, P.velocity.X, P.velocity.Y, ModContent.ProjectileType<TheSun>(), (int)(O.GetDamage(DamageClass.Magic).ApplyTo(O.inventory[O.selectedItem].damage)), 3f, P.owner);
    //        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Pulse"), P.position);
    //    }
    //    if (CHARGE > 251)
    //        CHARGE = 251;
    //    float MY = Main.mouseY + Main.screenPosition.Y;
    //    float MX = Main.mouseX + Main.screenPosition.X;
    //    if (Main.myPlayer == P.owner)
    //    {
    //        float num119 = 0f;
    //        if (O.inventory[O.selectedItem].shoot == P.type)
    //        {
    //            num119 = O.inventory[O.selectedItem].shootSpeed * P.scale;
    //        }
    //        Vector2 vector14 = new Vector2(O.position.X + (float)O.width * 0.5f, O.position.Y + (float)O.height * 0.5f);
    //        float num120 = MX - vector14.X;
    //        float num121 = MY - vector14.Y;
    //        float num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
    //        num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
    //        num122 = num119 / num122;
    //        num120 *= num122;
    //        num121 *= num122;
    //        if (num120 != P.velocity.X || num121 != P.velocity.Y)
    //        {
    //            P.netUpdate = true;
    //        }
    //        P.velocity.X = num120;
    //        P.velocity.Y = num121;
    //        STUFF++;
    //        if (STUFF > 300)
    //            P.Kill();
    //    }
    //    float targetrotation = (float)Math.Atan2((MY - O.position.Y), (MX - O.position.X));
    //    P.rotation = targetrotation;
    //    O.itemTime = 2;
    //    O.itemAnimation = 2;
    //    float Dist = 60;
    //    P.Center = Main.player[Projectile.owner].Center + new Vector2(60, 0).RotatedBy(Projectile.rotation); //new Vector2(O.itemLocation.X + (float)((float)Math.Cos(targetrotation) * Dist) * 0.66f, O.itemLocation.Y + (float)((float)Math.Sin(targetrotation) * Dist) * 0.66f);
    //    if (P.velocity.X < 0)
    //    {
    //        P.direction = -1;
    //    }
    //    else
    //    {
    //        P.direction = 1;
    //    }
    //    P.spriteDirection = P.direction;
    //    O.heldProj = P.whoAmI;
    //    O.direction = P.direction;
    //    O.itemRotation = (float)Math.Atan2((MY - O.position.Y) * O.direction, (MX - O.position.X) * O.direction) - 0.05f * O.direction;
    //}
    #endregion old
}
