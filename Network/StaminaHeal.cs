using System.IO;
using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Network;

public class StaminaHeal
{
    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        int pid = reader.ReadInt32();
        int healAmt = reader.ReadInt32();
        if (pid != Main.myPlayer)
        {
            Main.player[pid].GetModPlayer<ExxoStaminaPlayer>().StaminaHealEffect(healAmt, true);
        }
        if (Main.netMode == NetmodeID.Server)
        {
            ModPacket packet = MessageHandler.GetPacket(MessageID.StaminaHeal);
            packet.Write(pid);
            packet.Write(healAmt);
            packet.Send(-1, fromWho);
        }
    }
}
