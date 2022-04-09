using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Fury : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fury");
        Description.SetDefault("20% increased critical damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoPlayer>().CritDamageMult += 0.2f;
    }
}
