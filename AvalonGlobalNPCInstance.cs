using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonGlobalNPCInstance : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool AstigSpawned { get; set; }
    public int BleedStacks { get; set; } = 1;
    public bool Bleeding { get; set; }
    public bool IsBleedingHMBleed { get; set; }
    public int BreathCd { get; set; } = 45;
    public bool DlBreath { get; set; }
    public bool Electrified { get; set; }
    public bool Frozen { get; set; }
    public bool InfernaSpawned { get; set; }
    public bool JugRunOnce { get; set; }
    public bool LavaWalk { get; set; }
    public bool Malaria { get; set; }
    public bool NecroticDrain { get; set; }
    public bool NoOneHitKill { get; set; }
    public int ORebirth { get; set; }
    public bool Silenced { get; set; }
    public int SlimeHitCounter { get; set; }
    public bool Slowed { get; set; }
    public int SpikeTimer { get; set; }
    public bool Virulent { get; set; }
    public bool Inferno { get; set; }

    public override void ResetEffects(NPC npc)
    {
        NecroticDrain = false;
        Malaria = false;
        Electrified = false;
        Bleeding = false;
        Virulent = false;
        Inferno = false;
        //BleedStacks = 1;
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        if (Malaria)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 6;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Inferno)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (NecroticDrain)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 90;
            if (damage < 3)
            {
                damage = 3;
            }
        }
        if (Electrified)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            if (npc.velocity != Vector2.Zero)
                npc.lifeRegen -= 30;
            npc.lifeRegen -= 40;
            if (damage < 6)
            {
                damage = 6;
            }
        }
        if (Bleeding)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            int mult = 4;
            if (IsBleedingHMBleed)
            {
                mult = 6;
            }
            npc.lifeRegen -= mult * BleedStacks;
            Main.NewText(BleedStacks);
            if (damage < BleedStacks)
            {
                damage = BleedStacks;
            }
        }
        if (Virulent)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 100;
            if (damage < 10)
            {
                damage = 10;
            }
        }
    }
}
