using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BacteriaEndurance : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bacterial Endurance");
        Description.SetDefault("Thorns effect and increased damage and jump speed");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.jumpSpeedBoost += 0.2f;
    }
}
