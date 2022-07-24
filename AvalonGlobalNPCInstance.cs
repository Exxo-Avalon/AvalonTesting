using Terraria;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonGlobalNPCInstance : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool AstigSpawned { get; set; }
    public int BleedStacks { get; set; } = 1;
    public int BreathCd { get; set; } = 45;
    public bool DlBreath { get; set; }
    public bool Electrified { get; set; }
    public bool Frozen { get; set; }
    public bool InfernaSpawned { get; set; }
    public bool JugRunOnce { get; set; }
    public bool LavaWalk { get; set; }
    public bool Malaria { get; set; }
    public bool NoOneHitKill { get; set; }
    public int ORebirth { get; set; }
    public bool Silenced { get; set; }
    public int SlimeHitCounter { get; set; }
    public bool Slowed { get; set; }
    public int SpikeTimer { get; set; }

    public override void ResetEffects(NPC npc) => Malaria = false;

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
