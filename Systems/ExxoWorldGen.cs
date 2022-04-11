using System.Collections.Generic;
using AvalonTesting.World.Passes;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AvalonTesting.Systems;

public class ExxoWorldGen : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        GenPass currentPass;

        int underworld = tasks.FindIndex(genPass => genPass.Name == "Underworld");
        if (underworld != -1)
        {
            currentPass = new Underworld();
            tasks.Insert(underworld + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int shinies = tasks.FindIndex(genpass => genpass.Name == "Shinies");
        if (shinies != -1)
        {
            tasks[shinies] = new OreGenPreHardMode();
        }

        int smoothWorld = tasks.FindIndex(genPass => genPass.Name == "Smooth World");
        if (smoothWorld != -1)
        {
            currentPass = new SmoothWorld();
            tasks.Insert(smoothWorld + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int iceWalls = tasks.FindIndex(genpass => genpass.Name == "Cave Walls");
        if (iceWalls != -1)
        {
            currentPass = new Shrines();
            tasks.Insert(iceWalls + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int vines = tasks.FindIndex(genPass => genPass.Name == "Vines");
        if (vines != -1)
        {
            currentPass = new Impvines();
            tasks.Insert(vines + 1, currentPass);
            totalWeight += currentPass.Weight;
        }
    }
}
