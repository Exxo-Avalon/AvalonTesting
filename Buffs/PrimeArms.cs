using Avalon.Players;
using Avalon.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

// TODO: IMPLEMENT
public class PrimeArms : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Prime Arms");
        Description.SetDefault("The arms will fight for you");
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = false;
    }
    public override void Update(Player player, ref int buffIndex)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeArmsCounter>()] > 0)
        {
            player.GetModPlayer<ExxoSummonPlayer>().PrimeMinion = true;
        }
        if (!player.GetModPlayer<ExxoSummonPlayer>().PrimeMinion)
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
        else
        {
            player.buffTime[buffIndex] = 18000;
        }
    }
}
