using Terraria.ModLoader;

namespace ExxoAvalonOrigins;

public class ExxoAvalonOrigins : Mod
{
#if DEBUG
    public const bool DevMode = true;
#else
    public const bool DevMode = false;
#endif
    public new readonly Version Version = new(1, 0, 0, 0, DevMode);

    // Reference to the main instance of the mod
    public static ExxoAvalonOrigins Mod { get; private set; }

    public ExxoAvalonOrigins()
    {
        Mod = this;
    }
}
