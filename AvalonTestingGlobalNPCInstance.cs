using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalNPCInstance : GlobalNPC
{
    public bool Malaria;
    public bool IsBleedingHMBleed = false;
    public bool jugRunonce = false;
    public bool lavaWalk = false;
    public bool electrified = false;
    public bool frozen = false;
    public int breathCD = 45;
    public bool dlBreath = false;
    public bool silenced = false;
    public bool astigSpawned = false;
    public bool infernaSpawned = false;
    public int oRebirth = 0;
    public int slimeHitCounter = 0;
    public bool noOneHitKill = false;
    public int spikeTimer;
    public bool malaria;
    public bool bleeding;
    public int bleedStacks = 1;
    public bool slowed = false;
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
