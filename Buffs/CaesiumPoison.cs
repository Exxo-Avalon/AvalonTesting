using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Avalon.Buffs;

public class CaesiumPoison : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Poisoning");
        Description.SetDefault("You are poisoned by the Caesium");
        Main.debuff[Type] = true;
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
                player.KillMe(PlayerDeathReason.ByCustomReason(" was poisoned by the Caesium."), 10, 0);
            }
        }
        player.lifeRegenTime = 0;
        if (player.buffTime[buffIndex] == 0)
        {
            timer = 0;
        }
        player.GetModPlayer<ExxoBuffPlayer>().CaesiumPoison = true;
        player.blind = true;
    }
}
