using System.Collections.Generic;

namespace AvalonTesting.Network;

public static class NetworkManager
{
    public static readonly List<IPacketHandler> RegisteredHandlers = new();
}
