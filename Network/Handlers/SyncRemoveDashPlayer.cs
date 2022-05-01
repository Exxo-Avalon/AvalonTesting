using System.IO;
using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Network.Handlers;

[Autoload]
public class SyncRemoveDashPlayer : PacketHandler<SyncRemoveDashPlayer.HandlerArgs>
{
    protected override HandlerArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        ExxoDashPlayer dashPlayer = player.GetModPlayer<ExxoDashPlayer>();
        int key = reader.ReadInt32();
        if (dashPlayer.ActiveDashes.ContainsKey(key))
        {
            dashPlayer.ActiveDashes.Remove(key);
        }

        return new HandlerArgs(player, key);
    }

    protected override void Send(ModPacket packet, HandlerArgs args)
    {
        Player player = args.Player;
        packet.Write((byte)player.whoAmI);
        packet.Write(args.Key);
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
