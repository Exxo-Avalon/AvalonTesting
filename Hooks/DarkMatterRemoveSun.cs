using Avalon.Common;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Hooks;
public class DarkMatterRemoveSun : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Main.DrawSunAndMoon += OnDrawSunAndMoon;
    }
    private static void OnDrawSunAndMoon(On.Terraria.Main.orig_DrawSunAndMoon orig, Main self, Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
    {
        if (!Main.gameMenu)
        {
            if (!Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter && !Main.LocalPlayer.GetModPlayer<Players.ExxoPlayer>().DarkMatterMonolith)
            {
                orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
            }
        }
        else
            orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);

        //if (Main.LocalPlayer != null)
        //{
        //    if (ModContent.GetInstance<Systems.BiomeTileCounts>().DarkTiles < 300)//!Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter)
        //    {
        //        orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
        //    }
        //}
        //else orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
    }
}
