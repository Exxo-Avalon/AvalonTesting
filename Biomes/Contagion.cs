using AvalonTesting.Backgrounds;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class Contagion : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    public override int Music
    {
        get
        {
            if (Main.LocalPlayer.ZoneNormalUnderground || Main.LocalPlayer.ZoneNormalCaverns)
            {
                return AvalonTesting.MusicMod != null
                    ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/UndergroundContagion")
                    : MusicID.UndergroundCrimson;
            }

            return AvalonTesting.MusicMod != null
                ? MusicLoader.GetMusicSlot(AvalonTesting.MusicMod, "Sounds/Music/Contagion")
                : MusicID.Crimson;
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
