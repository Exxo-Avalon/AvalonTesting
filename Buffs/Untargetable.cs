using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Untargetable : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Untargetable");
        Description.SetDefault("You are untargetable");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.aggro -= 10000;
        player.immune = true;
        player.immuneAlpha = 0;
    }
    public override bool RightClick(int buffIndex) => false;
}
