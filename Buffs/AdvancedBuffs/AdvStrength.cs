using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvStrength : ModBuff
{
    private const float DamagePercentIncrease = 0.13f;
    private const int CritChancePercentIncrease = 2;
    private const int DefenseIncrease = 7;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Strength");
        Description.SetDefault("Increases stats");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += DamagePercentIncrease;
        player.GetCritChance<GenericDamageClass>() += CritChancePercentIncrease;
        player.statDefense += DefenseIncrease;
        player.lifeRegen++;
        player.GetModPlayer<ExxoPlayer>().AllCritDamage(0.1f);
    }
}
