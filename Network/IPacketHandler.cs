using System.IO;

namespace AvalonTesting.Network;

public interface IPacketHandler
{
    public void Handle(BinaryReader reader, int fromWho);
}
