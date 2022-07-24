using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Electrified : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Electrified");
        Description.SetDefault("Losing more life when moving");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Electrified = true;
    }
}
