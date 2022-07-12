using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Virulent : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Infected");
        Description.SetDefault("???");
        Main.debuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        if (player.lifeRegen > 0)
        {
            player.lifeRegen = 0;
        }

        player.lifeRegenTime = 0;
        player.lifeRegen -= 16;
    }
    public override void Update(NPC npc, ref int buffIndex)
    {
    }
}
