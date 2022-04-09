using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalNPCInstance : GlobalNPC
{
    public bool Malaria;
    public override bool InstancePerEntity => true;

    public override void ResetEffects(NPC npc)
    {
        Malaria = false;
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Malaria)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 30;
            if (damage < 2)
            {
                damage = 2;
            }
        }
    }
}
