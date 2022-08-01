using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;
public class Plagued : ModBuff
{
    private int def;
    private int dmg;
    public override void Update(NPC npc, ref int buffIndex)
    {
        if (npc.buffTime[buffIndex] == 60 * 10 - 1)
        {
            def = npc.defense;
            dmg = npc.damage;
        }
        npc.defense = (int)((float)(def * 2f / 3f));
        npc.damage = (int)((float)(dmg * 2f / 3f));
        if (npc.buffTime[buffIndex] == 1)
        {
            npc.defense = def;
            npc.damage = dmg;
        }
    }
}
