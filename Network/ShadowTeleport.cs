using System.IO;
using Terraria.ModLoader;

namespace Avalon.Network;

public static class ShadowTeleport
{
    public static void SendPacket(int teleportType = 0)
    {
        ModPacket message = MessageHandler.GetPacket(MessageID.ShadowTeleport);
        message.Write(teleportType);
        message.Send();
    }

    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        Logic.ShadowTeleport.Teleport(reader.ReadInt32(), true, fromWho);
    }
}
