using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class DarkMatter : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DarkMatter");

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter;
    }
}
