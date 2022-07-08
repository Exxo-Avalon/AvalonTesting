using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

// TODO: IMPLEMENT
public class SkyBlessing : ModBuff
{
    private int stacks = 1;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blessing of the Sky");
        Description.SetDefault("Minion damage is increased by ");
    }

    public override void ModifyBuffTip(ref string tip, ref int rare)
    {
        if (stacks < 10)
        {
            tip += (stacks * 2) + "%";
        }
        else tip += "25%";
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().SkyBlessing = true;
        stacks = player.GetModPlayer<ExxoBuffPlayer>().SkyStacks;
        if (player.buffTime[buffIndex] == 0)
        {
            player.GetModPlayer<ExxoBuffPlayer>().SkyStacks = 1;
        }
    }
}
