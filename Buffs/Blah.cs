using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Blah : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah");
        Description.SetDefault("You shouldn't be affected by this");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Lucky = true;
        player.enemySpawns = true;
        player.GetModPlayer<ExxoBuffPlayer>().AdvancedBattle = true;
        player.maxMinions += 4;
        player.accMerman = true;
        player.lavaImmune = true;
        player.cratePotion = true;
        player.fishingSkill += 100;
        player.statDefense += 10;
        player.endurance += 0.2f;
        player.ammoPotion = true;
        player.dangerSense = true;
        player.sonarPotion = true;
    }
}
