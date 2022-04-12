using AvalonTesting.Players;
using AvalonTesting.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class StingerProbe : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stinger Probe");
        Description.SetDefault("'Don't get too close!'");
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.dead || !player.active)
        {
            return;
        }

        if (player.GetModPlayer<ExxoBuffPlayer>().StingerProbeTimer < 300)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<StingerProbeMinion>()] < 4)
            {
                player.GetModPlayer<ExxoBuffPlayer>().StingerProbeTimer++;
            }

            return;
        }

        if (player.whoAmI != Main.myPlayer)
        {
            return;
        }

        Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.Center, Vector2.Zero,
            ModContent.ProjectileType<StingerProbeMinion>(), (int)(player.HeldItem.damage * 0.75f), 0f,
            player.whoAmI);
        player.GetModPlayer<ExxoBuffPlayer>().StingerProbeTimer = 0;
    }
}
