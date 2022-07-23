using AvalonTesting.Backgrounds;
using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class UndergroundContagion : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => ModContent.GetInstance<Contagion>().BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music
    {
        get
        {
            return MusicLoader.GetMusicSlot(Mod, "Sounds/Music/UndergroundContagion");
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
        return !player.ZoneDungeon && ModContent.GetInstance<Systems.BiomeTileCounts>().ContagionTiles > 200 && player.ZoneRockLayerHeight;
    }
}