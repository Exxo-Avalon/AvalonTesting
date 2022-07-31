using System;
using Avalon.Buffs;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonGlobalProjectile : GlobalProjectile
{
    public static int FindClosestHostile(Vector2 pos, float dist)
    {
        int closest = -1;
        float last = dist;
        for (int i = 0; i < Main.projectile.Length; i++)
        {
            Projectile p = Main.projectile[i];
            if (!p.active || !p.hostile)
            {
                continue;
            }

            if (Vector2.Distance(pos, p.Center) < last)
            {
                last = Vector2.Distance(pos, p.Center);
                closest = i;
            }
        }

        return closest;
    }

    public static int HowManyProjectiles(int min, int max)
    {
        int output = min;
        for (int i = min; i < max; i++)
        {
            if (Main.rand.NextBool(2 ^ (max - i)))
            {
                output++;
            }
        }

        return output;
    }

    public static Vector2 RotateAboutOrigin(Vector2 point, float rotation)
    {
        if (rotation < 0f)
        {
            rotation += 12.566371f;
        }

        Vector2 value = point;
        if (value == Vector2.Zero)
        {
            return point;
        }

        float num = (float)Math.Atan2(value.Y, value.X);
        num += rotation;
        return value.Length() * new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
    }

    public override bool CanHitPlayer(Projectile projectile, Player target)
    {
        if (target.HasBuff<BeeSweet>() && projectile.type == ProjectileID.Stinger)
        {
            return false;
        }

        return base.CanHitPlayer(projectile, target);
    }
    public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
    {
        if (Main.player[projectile.owner].Avalon().skyBlessing && Main.rand.NextBool(15) && projectile.minion)
        {
            int item = Item.NewItem(target.GetSource_DropAsItem(), projectile.getRect(), ModContent.ItemType<Items.Other.SkyInsignia>());
        }
    }
    public override void AI(Projectile projectile)
    {
        Player p = Main.player[projectile.owner];
        if (p.GetModPlayer<ExxoEquipEffectPlayer>().FrostGauntlet && projectile.DamageType == DamageClass.Melee)
        {
            Rectangle hitbox = projectile.Hitbox;
            int d = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, 0f, 0f, 100, default, 2.2f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 2f;
        }
        if (p.GetModPlayer<ExxoEquipEffectPlayer>().TerraClaws && projectile.DamageType == DamageClass.Melee)
        {
            Rectangle hitbox = projectile.Hitbox;
            int rn = Main.rand.Next(5);
            switch (rn)
            {
                case 0:
                    rn = DustID.Poisoned;
                    break;
                case 1:
                    rn = DustID.IceTorch;
                    break;
                case 2:
                    rn = DustID.Torch;
                    break;
                case 3:
                    rn = DustID.VenomStaff;
                    break;
                case 4:
                    rn = DustID.IchorTorch;
                    break;
            }
            int d = Dust.NewDust(new(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, rn, 0f, 0f, 100, default, 2.2f);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 2f;
        }
        if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Piercing>()) && projectile.penetrate != -1)
        {
            if (!projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().PiercingUp)
            {
                projectile.penetrate++;
                projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().PiercingUp = true;
            }
        }
    }
    public override void NumGrappleHooks(Projectile projectile, Player player, ref int numHooks)
    {
        if (projectile.type != ProjectileID.Web && player.GetModPlayer<ExxoPlayer>().HookBonus)
        {
            numHooks = 5;
        }
    }
}
