using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Electrified : ModBuff
{
    private int timer;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Electrified");
        Description.SetDefault("Losing more life when moving");
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
            if (player.velocity.Length() != 0)
            {
                amt += 3;
            }
            player.statLife -= amt;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.LifeRegen, amt, dramatic: false, dot: true);
        }
        player.lifeRegenTime = 0;
        if (player.buffTime[buffIndex] == 0)
        {
            timer = 0;
        }
        player.GetModPlayer<ExxoBuffPlayer>().Electrified = true;
    }
}
