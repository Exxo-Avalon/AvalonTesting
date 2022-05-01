using System.IO;
using AvalonTesting.Players;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Network.Handlers;

[Autoload]
public class SyncStingerProbe : PacketHandler<BasicPlayerNetworkArgs>
{
    protected override BasicPlayerNetworkArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        buffPlayer.StingerProbeRotation = reader.ReadSingle();
        return new BasicPlayerNetworkArgs(player);
    }

    protected override void Send(ModPacket packet, BasicPlayerNetworkArgs args)
    {
        Player player = args.Player;
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.Write(buffPlayer.StingerProbeRotation);
    }
}
