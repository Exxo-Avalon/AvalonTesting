using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Malaria : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Malaria");
        Description.SetDefault("Itchy Bastards");
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<ExxoBuffPlayer>().Malaria = true;
    }

    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonTestingGlobalNPCInstance>().Malaria = true;
    }
}
