using System;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Trophy;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Linq;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Avalon.Players;
using ReLogic.Content;


namespace Avalon.Projectiles.Melee;

public class SunkenSlammer : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("SunkenSlammer");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 4;
    }
    public Player player => Main.player[Projectile.owner];
    public int SwingSpeed = 50;
    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.aiStyle = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.scale = 1.5f;
        Projectile.ownerHitCheck = true;
        Projectile.timeLeft = SwingSpeed;
    }
    public Vector2 swingRadius = Vector2.Zero;
    public bool firstFrame = true;
    public float swordVel;
    public float posY;
    public override void AI()
    {
        player.heldProj = Projectile.whoAmI;

        if (firstFrame)
        {
            Vector2 toMouse = Vector2.Normalize(Main.MouseWorld - player.Center) * player.direction;
            posY = player.Center.Y - Projectile.Center.Y;
            posY = MathF.Sign(posY);
            swingRadius = Projectile.Center - player.Center;
            swingRadius = swingRadius.RotatedBy(toMouse.ToRotation());
            firstFrame = false;
        }

        swingRadius = swingRadius.RotatedBy(MathF.PI * 3 * swordVel / SwingSpeed * player.direction * posY);

        if (Projectile.timeLeft < 5)
        {
            swingRadius *= 0.95f;
        }

        swordVel = MathHelper.Lerp(0f, 2f, Projectile.timeLeft / (float)SwingSpeed);

        Projectile.Center = swingRadius + player.Center;

        Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + (45 * (MathHelper.Pi / 180));
        player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi);
    }
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        if (crit)
        {
            for (int i = 0; i < 3 + Main.rand.Next(3); i++)
            {
                float speed = 8f;
                Vector2 randVec = new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-50, -100));
                Vector2 randPos = Vector2.Normalize(randVec) * speed;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, randPos, ModContent.ProjectileType<Melee.UrchinSpike>(), 0, 2f, player.whoAmI);
            }
        }
        float diff = target.Center.X - player.Center.X;
        if (diff > 0)
        {
            hitDirection = 1;
        }
        if (diff < 0)
        {
            hitDirection = -1;
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/SunkenSlammer", AssetRequestMode.ImmediateLoad).Value;
        Texture2D after = ModContent.Request<Texture2D>("Avalon/Projectiles/Melee/SunkenSlammer_after", AssetRequestMode.ImmediateLoad).Value;

        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Vector2 offset = new Vector2(10f, -8f);

        for (int i = 0; i < Projectile.oldPos.Length; i++)
        {
            Vector2 drawPosOld = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
            Main.EntitySpriteDraw(after, drawPosOld, frame, lightColor * (1 - (i * 0.25f)) * 0.25f, Projectile.oldRot[i], frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, lightColor, Projectile.rotation, frame.Size() / 2f + offset, Projectile.scale, SpriteEffects.None, 0);

        return false;
    }
}
