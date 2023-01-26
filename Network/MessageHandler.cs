using System.IO;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Avalon.Network;

public class MessageHandler
{
    public static ModPacket GetPacket(MessageID messageID, int capacity = 256)
    {
        ModPacket message = Avalon.Mod.GetPacket(capacity);
        message.Write((int)messageID);
        return message;
    }

    public static void HandlePacket(BinaryReader reader, int fromWho)
    {
        MessageID id = (MessageID)reader.ReadInt32();
        switch (id)
        {
            case MessageID.ShadowTeleport:
                ShadowTeleport.HandlePacket(reader, fromWho);
                break;
            case MessageID.CursorPosition:
                CursorPosition.HandlePacket(reader, fromWho);
                break;
            case MessageID.SyncTime:
                SyncTime.HandlePacket(reader, fromWho);
                break;
            case MessageID.SyncWiring:
                SyncWiring.HandlePacket(reader, fromWho);
                break;
            case MessageID.StaminaHeal:
                StaminaHeal.HandlePacket(reader, fromWho);
                break;
        }
    }
}
