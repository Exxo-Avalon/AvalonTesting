using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Melting : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Melting");
        Description.SetDefault("I'm melting...!");
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Melting = true;
    }
}
