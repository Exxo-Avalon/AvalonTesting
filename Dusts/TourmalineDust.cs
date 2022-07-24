using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Avalon.Dusts;

public class TourmalineDust : ModDust
{
    public override bool Update(Dust dust)
    {
        var lightFade = (dust.scale > 1 ? 1 : dust.scale);
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), ((float)22 / 255 * lightFade), ((float)212 / 255 * lightFade), ((float)198 / 255 * lightFade));
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
