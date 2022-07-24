using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BloodCast : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blood Casting");
        Description.SetDefault("Maximum life is added to maximum mana");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += player.statLifeMax2;
    }
}
