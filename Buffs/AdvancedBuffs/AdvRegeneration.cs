using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvRegeneration : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Regeneration");
        Description.SetDefault("Provides life regeneration");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.lifeRegen += 3;
    }
}
