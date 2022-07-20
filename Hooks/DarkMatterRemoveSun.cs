using Terraria;
using AvalonTesting.Common;
using Microsoft.Xna.Framework;

namespace AvalonTesting.Hooks;
public class DarkMatterRemoveSun : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Main.DrawSunAndMoon += OnDrawSunAndMoon;
    }
    private static void OnDrawSunAndMoon(On.Terraria.Main.orig_DrawSunAndMoon orig, Main self, Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
    {
        if (Main.LocalPlayer != null)
        {
            if (!Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter)
            {
                orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
            }
        }
        else orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
    }
}
