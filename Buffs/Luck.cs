using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Luck : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Luck");
        Description.SetDefault("Doubles rare drop chance");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Lucky = true;
        player.enemySpawns = true;
        player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle = true;
    }
}
