using System.IO;
using AvalonTesting.Players;
using Terraria;

namespace AvalonTesting;

partial class AvalonTesting
{
    internal enum MessageType : byte
    {
        BuffPlayerSyncPlayer
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        var msgType = (MessageType)reader.ReadByte();

        switch (msgType)
        {
            case MessageType.BuffPlayerSyncPlayer:
                byte playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncPlayer(reader);
                break;
            default:
                Logger.Warn($"Unknown Message type: {msgType}");
                break;
        }
    }
}
