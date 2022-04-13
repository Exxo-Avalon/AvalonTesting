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

        switch (msgType)
        {
            case MessageType.BuffPlayerLazySync:
            {
                byte playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncPlayer(reader);
                break;
            }
            case MessageType.ExxoPlayerManualSyncMouse:
            {
                byte playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoPlayer>().HandleSyncMouse(reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoPlayer>().SyncMouse(whoAmI);
                }

                break;
            }
            case MessageType.ExxoBuffPlayerSyncDaggerStaff:
            {
                byte playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncDaggerStaff(reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().SyncDaggerStaff(whoAmI);
                }

                break;
            }
            case MessageType.ExxoBuffPlayerSyncStingerProbe:
            {
                byte playerIndex = reader.ReadByte();
                Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().HandleSyncStingerProbe(reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoBuffPlayer>().SyncStingerProbe(whoAmI);
                }

                break;
            }
            case MessageType.ExxoDashPlayerSyncActiveDash:
            {
                byte playerIndex = reader.ReadByte();
                int key = reader.ReadInt32();
                Main.player[playerIndex].GetModPlayer<ExxoDashPlayer>().HandleSyncDashPlayer(key, reader);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoDashPlayer>().SyncDashPlayer(key, whoAmI);
                }

                break;
            }
            case MessageType.ExxoDashPlayerSyncRemoveDash:
            {
                byte playerIndex = reader.ReadByte();
                int key = reader.ReadInt32();
                Main.player[playerIndex].GetModPlayer<ExxoDashPlayer>().HandleSyncRemoveDashPlayer(key);
                if (Main.netMode == NetmodeID.Server)
                {
                    Main.player[playerIndex].GetModPlayer<ExxoDashPlayer>().SyncRemoveDashPlayer(key, whoAmI);
                }

                break;
            }
            default:
                Logger.Warn($"Unknown Message type: {msgType}");
                break;
        }
    }

    internal enum MessageType : byte
    {
        BuffPlayerLazySync,
        ExxoPlayerManualSyncMouse,
        ExxoBuffPlayerSyncStingerProbe,
        ExxoBuffPlayerSyncDaggerStaff,
        ExxoDashPlayerSyncActiveDash,
        ExxoDashPlayerSyncRemoveDash
    }
}
