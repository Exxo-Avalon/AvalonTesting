using System.IO;
using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Network.Handlers;

[Autoload]
public class ExxoBuffPlayerSyncHandler : PacketHandler<BasicPlayerNetworkArgs>
{
    protected override BasicPlayerNetworkArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        buffPlayer.DaggerStaffRotation = reader.ReadSingle();
        buffPlayer.StingerProbeRotation = reader.ReadSingle();
        buffPlayer.StingerProbeTimer = reader.ReadInt32();
        return new BasicPlayerNetworkArgs(player);
    }

    protected override void Send(ModPacket packet, BasicPlayerNetworkArgs args)
    {
        Player player = args.Player;
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.Write(buffPlayer.DaggerStaffRotation);
        packet.Write(buffPlayer.StingerProbeRotation);
        packet.Write(buffPlayer.StingerProbeTimer);
    }
}
