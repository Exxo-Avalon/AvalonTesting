using Terraria.ModLoader;

namespace AvalonTesting.Buffs.AdvancedBuffs;

public class AdvFury : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Fury");
        Description.SetDefault("30% increased critical damage");
    }

    // public override void Update(Player player, ref int buffIndex)
    // {
    //     player.Avalon().critDamageMult += 0.3f;
    // }
}
