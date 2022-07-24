using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class SkyFortress : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SkyFortress");

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneSkyFortress;
    }
}
