using Avalon.Backgrounds;
using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class DarkMatterMonolith : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    public override string BestiaryIcon => base.BestiaryIcon;
    public override string BackgroundPath => "Avalon/Backgrounds/DarkMatter/DarkMatterBG";
    public override int Music => Main.curMusic;
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
        return player.GetModPlayer<ExxoPlayer>().DarkMatterMonolith;
    }
}
