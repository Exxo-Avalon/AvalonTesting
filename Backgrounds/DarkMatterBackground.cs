using Terraria.ModLoader;

namespace Avalon.Backgrounds;

public class DarkMatterBackground : ModSurfaceBackgroundStyle
{
    public override void ModifyFarFades(float[] fades, float transitionSpeed)
    {
        for (int i = 0; i < fades.Length; i++)
        {
            if (i == Slot)
            {
                fades[i] += transitionSpeed;
                if (fades[i] > 1f)
                {
                    fades[i] = 1f;
                }
            }
            else
            {
                fades[i] -= transitionSpeed;
                if (fades[i] < 0f)
                {
                    fades[i] = 0f;
                }
            }
        }
    }

    /*public override int ChooseFarTexture()
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/DarkMatter/DarkMatterBG");
    }
    public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
    {
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.PointClamp, null, null);
        return true;
    }
    private static int SurfaceFrameCounter;
    private static int SurfaceFrame;
    public override int ChooseMiddleTexture()
    {
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/DarkMatter/DarkMatterBG");
    }*/

    public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
    {
        b -= 500;
        //a -= 500;
        //scale = 2f;
        parallax = 0;
        return ModContent.GetModBackgroundSlot($"{Mod.Name}/Backgrounds/DarkMatter/DarkMatterBG");
    }
}
