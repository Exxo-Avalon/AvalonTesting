using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class PointingDust : ModDust
{
    public override bool Update(Dust dust)
    {
        //Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), (dust.color.R / 255f) / 2f, (dust.color.G / 255f) / 2f, (dust.color.B / 255f) / 2f);
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
