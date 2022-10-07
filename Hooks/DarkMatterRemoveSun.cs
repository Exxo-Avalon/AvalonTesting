using Avalon.Common;
using Avalon.Players;
using Microsoft.Xna.Framework;
using On.Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class DarkMatterRemoveSun : ModHook
{
    protected override void Apply() => Main.DrawSunAndMoon += OnDrawSunAndMoon;

    private static void OnDrawSunAndMoon(Main.orig_DrawSunAndMoon orig, Terraria.Main self,
                                         Terraria.Main.SceneArea sceneArea, Color moonColor, Color sunColor,
                                         float tempMushroomInfluence)
    {
        if (Terraria.Main.gameMenu)
        {
            orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
            return;
        }

        if (Terraria.Main.LocalPlayer.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter)
        {
            return;
        }

        orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
    }
}
