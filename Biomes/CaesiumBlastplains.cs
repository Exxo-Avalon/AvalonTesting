using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class CaesiumBlastplains : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => AvalonTesting.Mod.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.Mod.MusicMod, "Sounds/Music/CaesiumBlastplains")
        : MusicID.Hell;

    public override bool IsBiomeActive(Player player)
    {
        return player.AvalonBiome().ZoneCaesium;
    }
}
