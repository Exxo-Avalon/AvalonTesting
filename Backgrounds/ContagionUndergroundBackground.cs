using Terraria.ModLoader;

namespace AvalonTesting.Backgrounds;

public class ContagionUndergroundBackground : ModUndergroundBackgroundStyle
{
    public override void FillTextureArray(int[] textureSlots)
    {
        textureSlots[1] =
            BackgroundTextureLoader.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground1");
        textureSlots[2] =
            BackgroundTextureLoader.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground2");
        textureSlots[3] =
            BackgroundTextureLoader.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground3");
        textureSlots[4] =
            BackgroundTextureLoader.GetBackgroundSlot($"{Mod.Name}/Backgrounds/ContagionUndergroundBackground4");
    }
}
