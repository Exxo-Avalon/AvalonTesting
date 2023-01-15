using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Players;

namespace Avalon.Buffs;

public class WardCurse : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ward Curse");
        Description.SetDefault("Losing life");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.buffTime[buffIndex] % 120 == 0)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " died from extra damage."), (player.statDefense / 2) + (player.GetModPlayer<ExxoBuffPlayer>().WardCurseDOT / 10 / 5), 0);
        }
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<ExxoBuffPlayer>().WardCurseDOT = 0;
            player.GetModPlayer<ExxoEquipEffectPlayer>().WardCD = 60 * 45;
        }
    }
}
