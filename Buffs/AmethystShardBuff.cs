using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class AmethystShardBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Defense Up");
        Description.SetDefault("Defense is increased by 6");
    }
    public override void Update(Player player, ref int buffIndex)
    {
        player.statDefense += 6;
    }
}
