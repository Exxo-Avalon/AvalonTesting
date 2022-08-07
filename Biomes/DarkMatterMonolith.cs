using Avalon.Backgrounds;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class DarkMatterMonolith : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
    //public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("Avalon/DarkMatterWaterStyle");
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => base.BackgroundPath;
    public override string MapBackground => BackgroundPath;

    public override int Music => Main.curMusic;

    public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<DarkMatterBackground>();

    public override void SpecialVisuals(Player player, bool isActive)
    {
        if (isActive)
        {
            Main.ColorOfTheSkies = new Color(126, 71, 107);
            player.ManageSpecialBiomeVisuals("Avalon:DarkMatter", isActive);
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<Systems.BiomeTileCounts>().DarkMonolithTiles > 1;
    }
}
