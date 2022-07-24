using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class FracturingArmor : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fracturing Armor");
        Description.SetDefault("Defense is decreased by ");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        tip += Main.LocalPlayer.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLevel;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense -= player.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLevel;
        if (player.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLastRecord <= player.buffTime[buffIndex])
        {
            player.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLastRecord = player.buffTime[buffIndex];
            if (player.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLevel < 30)
            {
                player.GetModPlayer<ExxoBuffPlayer>().FracturingArmorLevel += 3;
            }
        }
        //Main.buffTip[149] = "Decreased defense by " + player.GetModPlayer<AvalonGlobalPlayer>(mod).fAlevel;
    }
}
