using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class CaesiumBlastplains : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music => AvalonTesting.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/CaesiumBlastplains")
        : MusicID.Hell;

    public override bool IsBiomeActive(Player player) => player.AvalonBiome().ZoneCaesium;
}
