using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalNPCInstance : GlobalNPC
{
    public bool astigSpawned = false;
    public int BleedStacks = 1;
    public int breathCD = 45;
    public bool dlBreath = false;
    public bool electrified = false;
    public bool frozen = false;
    public bool infernaSpawned = false;
    public bool jugRunonce = false;
    public bool lavaWalk = false;
    public bool malaria;
    public bool Malaria;
    public bool noOneHitKill = false;
    public int oRebirth = 0;
    public bool silenced = false;
    public int slimeHitCounter = 0;
    public bool slowed = false;
    public int spikeTimer;
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
