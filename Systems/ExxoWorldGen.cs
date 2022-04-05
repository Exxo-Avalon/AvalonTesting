using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AvalonTesting.Systems;
public class ExxoWorldGen : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        int vines = tasks.FindIndex(genpass => genpass.Name == "Vines");
        int underworld = tasks.FindIndex(genpass => genpass.Name == "Underworld");
        if (underworld != -1)
        {
            tasks.Insert(underworld + 1, new PassLegacy("Avalon Underworld", World.Passes.Underworld.Method));
        }
        int smoothWorld = tasks.FindIndex(genpass => genpass.Name == "Smooth World");
        if (smoothWorld != -1)
        {
            tasks.Insert(smoothWorld + 1, new PassLegacy("Unsmoothing Hellcastle", World.Passes.SmoothWorld.Method));
        }
        if (vines != -1)
        {
            tasks.Insert(vines + 1, new PassLegacy("Impvines", World.Passes.Impvines.Method));
        }
    }
}
