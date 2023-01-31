using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;
public class SkylashProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.IsAWhip[Type] = true;
        DisplayName.SetDefault("Skylash");
    }
    public override void SetDefaults()
    {
        Projectile.DefaultToWhip();
        Projectile.WhipSettings.Segments = 20;
        Projectile.WhipSettings.RangeMultiplier = 2.5f;
    }
    int dusttimer;

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        int L = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + new Vector2(Main.rand.Next(-20,20),Main.rand.Next(-200,-150)),new Vector2(0,24).RotatedByRandom(MathHelper.Pi / 4),ModContent.ProjectileType<Lightning>(),Projectile.damage,Projectile.knockBack,Projectile.owner);
        Main.projectile[L].ai[1] = Main.rand.Next(100);
        Main.projectile[L].ai[0] = new Vector2(0, Main.rand.NextFloat(0, 20)).ToRotation();// * ((Main.rand.Next(0,1) * 2) - 1);
    }
    public override void PostAI()
    {
        dusttimer++;
        Player player = Main.player[Projectile.owner];
        List<Vector2> list = new List<Vector2>();
        Projectile.FillWhipControlPoints(Projectile, list);
        if (dusttimer is > 18 and < 35)
        {
            int d = Dust.NewDust(list[Projectile.WhipSettings.Segments], 0, 0, DustID.Cloud, 0, 0);
            Main.dust[d].noGravity = true;
            Main.dust[d].fadeIn = Main.rand.NextFloat(1,1.5f);
            Main.dust[d].alpha = 128;
            if (Main.rand.NextBool(2))
            {
                int d1 = Dust.NewDust(list[Projectile.WhipSettings.Segments], 0, 0, DustID.IceTorch, 0, 0);
                Main.dust[d1].fadeIn = Main.rand.NextFloat(0, 1.3f);
                Main.dust[d1].noGravity = true;
            }
            if (Main.rand.NextBool(3))
            {
                int d2 = Dust.NewDust(list[Projectile.WhipSettings.Segments], 0, 0, DustID.TintableDust, 0, 0);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].color = new Color(15, 167, 211, 128);
            }
        }
    }
    private void DrawLine(List<Vector2> list) // Thanks example mod
    {
        Texture2D texture = TextureAssets.FishingLine.Value;
        Rectangle frame = texture.Frame();
        Vector2 origin = new Vector2(frame.Width / 2, 2);

        Vector2 pos = list[0];
        for (int i = 0; i < list.Count - 1; i++)
        {
            Vector2 element = list[i];
            Vector2 diff = list[i + 1] - element;

            float rotation = diff.ToRotation() - MathHelper.PiOver2;
            Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.Cyan);
            Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

            Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

            pos += diff;
        }
    }
    public override bool PreDraw(ref Color lightColor) //Thanks example mod 2: Electric boogaloo
    {
        List<Vector2> list = new List<Vector2>();
        Projectile.FillWhipControlPoints(Projectile, list);

        DrawLine(list);

        SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        Main.instance.LoadProjectile(Type);
        Texture2D texture = TextureAssets.Projectile[Type].Value;

        Vector2 pos = list[0];

        for (int i = 0; i < list.Count - 1; i++)
        {
            // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
            // You can change them if they don't!
            Rectangle frame = new Rectangle(0, 0, 38, 28);
            Vector2 origin = new Vector2(19, 0);
            float scale = 1;

            // These statements determine what part of the spritesheet to draw for the current segment.
            // They can also be changed to suit your sprite.
            if (i == list.Count - 2)
            {
                frame.Y = 112;
                frame.Height = 28;
            }
            else if (i > 10)
            {
                frame.Y = 84;
                frame.Height = 28;
            }
            else if (i > 5)
            {
                frame.Y = 52;
                frame.Height = 28;
            }
            else if (i > 0)
            {
                frame.Y = 29;
                frame.Height = 28;
            }

            Vector2 element = list[i];
            Vector2 diff = list[i + 1] - element;

            float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
            Color color = Lighting.GetColor(element.ToTileCoordinates());

            Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

            pos += diff;
        }
        return false;
    }
}
