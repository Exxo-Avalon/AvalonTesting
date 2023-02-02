using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class testDust : ModDust
{
    public override bool Update(Dust dust)
    {
        dust.alpha += 3;
        return false;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        Color gray = new Color(25, 25, 25);
        Color purple = new Color(180, 180, 0);
        Color ret;
        if (dust.alpha < 60)
        {
            ret = Color.Lerp(new Color(180, 180, 0), purple, dust.alpha / 60f);
        }
        else if (dust.alpha < 120)
        {
            ret = Color.Lerp(purple, gray, (dust.alpha - 60) / 60f);
        }
        else
            ret = gray;
        return ret * ((255 - dust.alpha) / 255f);
    }
}
