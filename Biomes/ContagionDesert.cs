using Avalon.Backgrounds;
using Avalon.Players;
using Avalon.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class ContagionDesert : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/ContagionWaterStyle");
    public override string BestiaryIcon => ModContent.GetInstance<Contagion>().BestiaryIcon;
    public override string BackgroundPath => ModContent.GetInstance<Contagion>().BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music
    {
        get
        {
            return Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/Contagion") : MusicID.Crimson;
        }
    }
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle =>
        ModContent.GetInstance<ContagionSurfaceDesertBackground>();

    //public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    //{
    //    get
    //    {
    //        if (Main.LocalPlayer.ZoneSnow)
    //        {
    //            return ModContent.GetInstance<ContagionUndergroundSnowBackground>();
    //        }

    //        return ModContent.GetInstance<ContagionUndergroundBackground>();
    //    }
    //}

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<BiomeTileCounts>().ContagionDesertTiles > 350 && player.ZoneOverworldHeight;
    }
}
