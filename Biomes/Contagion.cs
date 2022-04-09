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
                return AvalonTesting.Mod.MusicMod != null
                    ? MusicLoader.GetMusicSlot(AvalonTesting.Mod.MusicMod, "Sounds/Music/UndergroundContagion")
                    : MusicID.UndergroundCrimson;
            }

            return AvalonTesting.Mod.MusicMod != null
                ? MusicLoader.GetMusicSlot(AvalonTesting.Mod.MusicMod, "Sounds/Music/Contagion")
                : MusicID.Crimson;
        }
    }

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneDesert)
            {
                return Mod.Find<ModSurfaceBackgroundStyle>("ContagionSurfaceDesertBackground");
            }

            return Mod.Find<ModSurfaceBackgroundStyle>("ContagionSurfaceBackground");
        }
    }

    public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
    {
        get
        {
            if (Main.LocalPlayer.ZoneSnow)
            {
                return Mod.Find<ModUndergroundBackgroundStyle>("ContagionUndergroundSnowBackground");
            }

            return Mod.Find<ModUndergroundBackgroundStyle>("ContagionUndergroundBackground");
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion;
    }
}
