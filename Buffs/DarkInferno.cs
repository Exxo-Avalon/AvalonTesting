using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Avalon.Buffs;

public class DarkInferno : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Dark Inferno");
        Description.SetDefault("Losing life to the darkness");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.lifeRegen > 0)
        {
            player.lifeRegen = 0;
        }
        timer++;
        if (timer % 6 == 0)
        {
            int amt = 3;
            if (player.GetModPlayer<ExxoEquipEffectPlayer>().DuraShield)
            {
                amt = 2;
            }
            else if (player.GetModPlayer<ExxoEquipEffectPlayer>().DuraOmegaShield)
            {
                amt = 1;
            }
            player.statLife -= amt;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, amt, dramatic: false, dot: true);
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(" withered away in the dark flames."), 10, 0);
            }
        }
        player.lifeRegenTime = 0;
        if (player.buffTime[buffIndex] == 0)
        {
            timer = 0;
        }
        player.GetModPlayer<ExxoBuffPlayer>().DarkInferno = true;
    }
}
