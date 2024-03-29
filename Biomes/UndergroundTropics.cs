using Avalon.Backgrounds;
using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class UndergroundTropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    public override string BestiaryIcon => ModContent.GetInstance<Tropics>().BestiaryIcon;
    public override string BackgroundPath => ModContent.GetInstance<Tropics>().BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music
    {
        get
        {
            return Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/UndergroundTropics") : MusicID.JungleUnderground;
        }
    }

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    {
        get
        {
            return ModContent.GetInstance<TropicsUndergroundBackground>();
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return !player.ZoneDungeon && ModContent.GetInstance<Systems.BiomeTileCounts>().TropicsTiles > 50 && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
    }
}
