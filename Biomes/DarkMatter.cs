using Avalon.Backgrounds;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class DarkMatter : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/DarkMatterWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;
    public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DarkMatter");
    public override void SpecialVisuals(Player player, bool isActive)
    {
        Main.ColorOfTheSkies = new Microsoft.Xna.Framework.Color(126, 71, 107);
        player.ManageSpecialBiomeVisuals("Avalon:DarkMatter", isActive);
    }
    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
    {
        get
        {
            return ModContent.GetInstance<DarkMatterBackground>();
        }
    }
    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter;
    }
}
