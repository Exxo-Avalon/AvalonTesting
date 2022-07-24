using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Fury : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fury");
        Description.SetDefault("200% increased critical damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoPlayer>().CritDamageMult += 2f;
    }
}
