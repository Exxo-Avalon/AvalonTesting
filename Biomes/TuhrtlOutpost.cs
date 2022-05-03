using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class TuhrtlOutpost : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    public override int Music => AvalonTesting.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/TuhrtlOutpost")
        : MusicID.Temple;

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneTuhrtlOutpost;
    }
}
