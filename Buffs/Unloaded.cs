using Terraria;
using Terraria.ModLoader;
using AvalonTesting.Players;

namespace AvalonTesting.Buffs;

public class Unloaded : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Unloaded");
        Description.SetDefault("You can't use ranged weapons");
        Main.debuff[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Unloaded = true;
    }
}
