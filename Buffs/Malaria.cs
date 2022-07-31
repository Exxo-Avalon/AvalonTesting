using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Malaria : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Malaria");
        Description.SetDefault("You have been bitten");
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.lifeRegen > 0)
        {
            player.lifeRegen = 0;
        }
        timer++;
        if (timer % 4 == 0)
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
        }
        player.lifeRegenTime = 0;
        if (player.buffTime[buffIndex] == 0)
        {
            timer = 0;
        }
        player.GetModPlayer<ExxoBuffPlayer>().Malaria = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().Malaria = true;
    }
}
