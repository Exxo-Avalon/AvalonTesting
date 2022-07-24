using System.IO;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Network.Handlers;

[Autoload]
public class SyncDashPlayer : PacketHandler<SyncDashPlayer.HandlerArgs>
{
    protected override HandlerArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        ExxoDashPlayer dashPlayer = player.GetModPlayer<ExxoDashPlayer>();
        int key = reader.ReadInt32();
        dashPlayer.ActiveDashes.Add(key,
            new ExxoDashPlayer.DashData((ExxoDashPlayer.DashDirection)reader.ReadByte(), 0, 0));
        return new HandlerArgs(player, key);
    }

    protected override void Send(ModPacket packet, HandlerArgs args)
    {
        Player player = args.Player;
        ExxoDashPlayer dashPlayer = player.GetModPlayer<ExxoDashPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.Write(args.Key);
        packet.Write((byte)dashPlayer.ActiveDashes[args.Key].Direction);
    }

    public class HandlerArgs : NetworkArgs
    {
        public readonly int Key;
        public readonly Player Player;

        public HandlerArgs(Player player, int key)
        {
            Player = player;
            Key = key;
        }
    }
}
