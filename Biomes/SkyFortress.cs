using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class SkyFortress : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => AvalonTesting.MusicMod != null
        ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/SkyFortress")
        : MusicID.Dungeon;

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress;
    }
}
