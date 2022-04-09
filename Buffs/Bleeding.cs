using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

// TODO: NEEDS IMPLEMENTATION
public class Bleeding : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bleeding");
        Description.SetDefault("Losing life");
        Main.debuff[Type] = true;
    }

    // public override void Update(NPC npc, ref int buffIndex)
    // {
    //     npc.GetGlobalNPC<ExxoAvalonOriginsGlobalNPCInstance>().bleeding = true;
    // }
}
