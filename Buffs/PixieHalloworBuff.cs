using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

// TODO: IMPLEMENT
public class PixieHalloworBuff : ModBuff
{
    private int origDmg;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pixie Buff");
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
        npc.damage = npc.defDamage;
        npc.defense = npc.defDefense;
        npc.damage = (int)(npc.damage * 1.3f);
        npc.defense = (int)(npc.defense * 1.5f);
    }
}
