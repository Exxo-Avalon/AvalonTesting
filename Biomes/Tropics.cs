using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class Tropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;

    public override int Music => AvalonTesting.Mod.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.Mod.MusicMod, "Sounds/Music/Tropics")
        : MusicID.Jungle;

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        Mod.Find<ModSurfaceBackgroundStyle>("TropicsSurfaceBackground");

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics;
    }
}
