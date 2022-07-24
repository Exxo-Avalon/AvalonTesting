using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvDangersense : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Dangersense");
        Description.SetDefault("You can see nearby hazards");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.dangerSense = true;
    }
}
