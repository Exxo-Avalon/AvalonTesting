using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Avalon.Dusts;

public class OpalDust : ModDust
{
    public override bool Update(Dust dust)
    {
        Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), Main.DiscoR / 255f, Main.DiscoG / 255f, Main.DiscoB / 255f);
        return true;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(255, 255, 255, 100);
    }
}
