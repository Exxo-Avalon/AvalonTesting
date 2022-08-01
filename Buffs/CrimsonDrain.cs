using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class CrimsonDrain : ModBuff
{
    private const int FrameInterval = 50;
    private const int MaxDistance = 620;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aura Drain");
        Description.SetDefault("On-screen enemies take damage");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        int pposX = (int)player.position.X;
        int pposY = (int)player.position.Y;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            NPC n = Main.npc[i];
            if (!n.townNPC && n.active && !n.dontTakeDamage && !n.friendly && n.life >= 1 &&
                n.position.X >= pposX - 620 && n.position.X <= pposX + 620 && n.position.Y >= pposY - 620 &&
                n.position.Y <= pposY + 620)
            {
                player.GetModPlayer<ExxoBuffPlayer>().FrameCount++;
                if (player.GetModPlayer<ExxoBuffPlayer>().FrameCount % FrameInterval == 0)
                {
                    for (int j = 0; j < Main.npc.Length; j++)
                    {
                        NPC n2 = Main.npc[j];
                        if (!n2.townNPC && n2.active && !n2.dontTakeDamage && !n2.friendly && n2.life >= 1 &&
                            n2.position.X >= pposX - 620 && n2.position.X <= pposX + 620 && n2.position.Y >= pposY - 620 &&
                            n2.position.Y <= pposY + 620)
                        {
                            n2.StrikeNPC(1, 0f, 1);
                        }
                    }
                }
            }
        }
    }
}
