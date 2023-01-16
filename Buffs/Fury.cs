using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Fury : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Fury");
        Description.SetDefault("20% increased critical damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoPlayer>().AllCritDamage(0.2f);
    }
}
