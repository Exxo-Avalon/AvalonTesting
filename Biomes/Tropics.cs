using Avalon.Backgrounds;
using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class Tropics : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeMedium;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/TropicsWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override int Music
    {
        get
        {
            if (Main.dayTime)
                return Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/DayTropics") : MusicID.Jungle;
            return Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/NightTropics") : MusicID.JungleNight;
        }
    }


    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<TropicsSurfaceBackground>();

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<Systems.BiomeTileCounts>().TropicsTiles > 200 && player.ZoneOverworldHeight;
    }
}
