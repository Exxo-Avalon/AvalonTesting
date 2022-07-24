using Avalon.Backgrounds;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class Tropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/TropicsWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Tropics");

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<TropicsSurfaceBackground>();

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneTropics;
    }
}
