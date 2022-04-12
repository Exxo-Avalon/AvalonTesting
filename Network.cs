using System.IO;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;

namespace AvalonTesting;

partial class AvalonTesting
{
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        var msgType = (MessageType)reader.ReadByte();
        byte playerIndex;

        switch (msgType)
        {
            case MessageType.BuffPlayerLazySync:
                playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncPlayer(reader);
                break;
            case MessageType.ExxoPlayerManualSyncMouse:
                playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoPlayer>().HandleSyncMouse(reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoPlayer>().SyncMouse(whoAmI);
                }

                break;
            case MessageType.ExxoBuffPlayerSyncStingerProbe:
                playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncStingerProbe(reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().SyncStingerProbe(whoAmI);
                }

                break;
            default:
                Logger.Warn($"Unknown Message type: {msgType}");
                break;
        }
    }

    internal enum MessageType : byte
    {
        BuffPlayerLazySync,
        ExxoPlayerManualSyncMouse,
        ExxoBuffPlayerSyncStingerProbe
    }
}
