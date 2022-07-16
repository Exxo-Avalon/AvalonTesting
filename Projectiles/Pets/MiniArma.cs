using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles.Pets;

public class MiniArma : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mini Arma");
        Main.projFrames[Projectile.type] = 12;
        Main.projPet[Projectile.type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.KingSlimePet);
        AIType = ProjectileID.KingSlimePet;
        Projectile.width = 36;
        Projectile.height = 34;
    }
    public override bool PreAI()
    {
        Player player = Main.player[Projectile.owner];
        player.petFlagKingSlimePet = false;
        return true;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];

        if (!player.dead && player.HasBuff(ModContent.BuffType<Buffs.MiniArma>()))
        {
            Projectile.timeLeft = 2;
        }
    }
}
