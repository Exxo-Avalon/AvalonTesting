using System.IO;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Network.Handlers;

[Autoload]
public class SyncDaggerStaff : PacketHandler<BasicPlayerNetworkArgs>
{
    protected override BasicPlayerNetworkArgs Handle(BinaryReader reader)
    {
        byte playerIndex = reader.ReadByte();
        Player player = Main.player[playerIndex];
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        buffPlayer.DaggerStaffRotation = reader.ReadSingle();
        return new BasicPlayerNetworkArgs(player);
    }

    protected override void Send(ModPacket packet, BasicPlayerNetworkArgs args)
    {
        Player player = args.Player;
        ExxoBuffPlayer buffPlayer = player.GetModPlayer<ExxoBuffPlayer>();
        packet.Write((byte)player.whoAmI);
        packet.Write(buffPlayer.DaggerStaffRotation);
    }
}
