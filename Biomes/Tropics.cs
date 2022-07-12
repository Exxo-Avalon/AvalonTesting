using AvalonTesting.Backgrounds;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class Tropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Tropics");

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<TropicsSurfaceBackground>();

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics;
    }
}
