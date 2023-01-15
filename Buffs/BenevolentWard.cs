using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BenevolentWard : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Benevolent Ward");
        Description.SetDefault("You are absorbing damage");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Ward = true;
        if (player.buffTime[buffIndex] == 1)
        {
            player.AddBuff(ModContent.BuffType<WardCurse>(), 20 * 60);
        }
    }
}
