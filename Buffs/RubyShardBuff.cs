using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class RubyShardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Armor Penetration Up");
        Description.SetDefault("Armor penetration is increased by 5");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetArmorPenetration(DamageClass.Generic) += 5;
    }
}
