using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvFeatherfall : ModBuff
{
    // TODO: IMPLEMENT
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Featherfall");
        Description.SetDefault("Press UP or DOWN to control speed of descent");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.slowFall = true;
    }
}
