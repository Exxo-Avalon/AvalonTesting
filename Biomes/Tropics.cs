using AvalonTesting.Backgrounds;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class Tropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

    public override int Music => AvalonTesting.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/Tropics")
        : MusicID.Jungle;

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<TropicsSurfaceBackground>();

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics;
    }
}
