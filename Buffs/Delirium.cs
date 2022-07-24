using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Delirium : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Curse of Delirium");
        Description.SetDefault("Experiencing random bouts of confusion");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();

        if (buffPlayer.DeleriumCount > 0)
        {
            player.confused = true;
            buffPlayer.DeleriumCount--;
        }
        else if (Main.rand.Next(600) == 0)
        {
            buffPlayer.DeleriumCount = Main.rand.Next(240, 481);
        }
    }
}
