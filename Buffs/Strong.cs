using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Strong : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Strength");
        Description.SetDefault("Increased stats");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetCritChance<GenericDamageClass>() += 1;
        player.statDefense += 5;
        player.lifeRegen++;
        player.GetModPlayer<ExxoPlayer>().CritDamageMult += 0.05f;
    }
}
