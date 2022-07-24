using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Berserk : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Berserk!");
        Description.SetDefault("True melee weapons deal 150% critical damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.HeldItem.shoot == ProjectileID.None)
        {
            player.GetModPlayer<ExxoPlayer>().CritDamageMult += 1.5f;
        }
    }
}
