using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class NecroticDrain : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Necrotic Drain");
        Description.SetDefault("Rapidly wasting away");
        Main.debuff[Type] = true;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.GetGlobalNPC<AvalonGlobalNPCInstance>().NecroticDrain = true;
    }
}
