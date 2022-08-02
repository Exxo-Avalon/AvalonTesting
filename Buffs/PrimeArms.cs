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
        if (player.ownedProjectileCounts[ModContent.ProjectileType<PriminiCannon>()] > 0 ||
            player.ownedProjectileCounts[ModContent.ProjectileType<PriminiLaser>()] > 0 ||
            player.ownedProjectileCounts[ModContent.ProjectileType<PriminiSaw>()] > 0 ||
            player.ownedProjectileCounts[ModContent.ProjectileType<PriminiVice>()] > 0)
        {
            player.buffTime[buffIndex] = 18000;
        }
        else
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
