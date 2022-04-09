using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

// TODO: IMPLEMENT
public class Hungry : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hungry");
        Description.SetDefault("The hungry will fight for you");
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = false;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        // if (player.ownedProjectileCounts[ModContent.ProjectileType<HungrySummon>()] > 0)
        // {
        //     player.buffTime[buffIndex] = 18000;
        // }
        // else
        // {
        //     player.DelBuff(buffIndex);
        //     buffIndex--;
        // }
    }
}
