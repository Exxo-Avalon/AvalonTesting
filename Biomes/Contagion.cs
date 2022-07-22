using AvalonTesting.Backgrounds;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class Contagion : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("AvalonTesting/ContagionWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music
    {
        get
        {
            //if (Main.LocalPlayer.ZoneNormalUnderground || Main.LocalPlayer.ZoneNormalCaverns)
            //{
            //    return MusicLoader.GetMusicSlot(Mod, "Sounds/Music/UndergroundContagion");
            //}

            return MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Contagion");
        }
    }
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneDesert)
            {
                return ModContent.GetInstance<ContagionSurfaceDesertBackground>();
            }

            return ModContent.GetInstance<ContagionSurfaceBackground>();
        }
    }

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneSnow)
            {
                return ModContent.GetInstance<ContagionUndergroundSnowBackground>();
            }

            return ModContent.GetInstance<ContagionUndergroundBackground>();
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion;
    }
}
