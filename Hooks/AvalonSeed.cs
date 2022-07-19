//using Terraria;
//using Terraria.ModLoader;
//using AvalonTesting.Common;

//namespace AvalonTesting.Hooks;
//// POSSIBLY USED FOR AVALON DRUNK WORLD SEED
//[Autoload(Side = ModSide.Both)]
//public class AvalonSeed : ModHook
//{
//    protected override void Apply()
//    {
//        On.Terraria.WorldGen.GenerateWorld += OnGenerateWorld;
//    }

//    private static void OnGenerateWorld(On.Terraria.WorldGen.orig_GenerateWorld orig, int seed, Terraria.WorldBuilding.GenerationProgress progress = null)
//    {
//        if (seed == 5232012)
//        {

//        }
//        else orig(seed, progress);
//    }
//}
