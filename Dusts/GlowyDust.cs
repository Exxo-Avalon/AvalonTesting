using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Dusts;

public class GlowyDust : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.scale = Main.rand.NextFloat(1.5f, 2f);
        dust.noGravity = true;
        dust.frame = new Rectangle(0, 0, 10, 10);
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(Main.rand.Next(0, 256), Main.rand.Next(0, 256), Main.rand.Next(0, 256), 0);
    }
    public override bool Update(Dust dust)
    {
        if (Main.rand.NextBool(3))
        {
            //dust.velocity = dust.velocity.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4));
        }
        dust.position += dust.velocity;
        //Lighting.AddLight(dust.position, new Vector3(0.2f, 0.2f, 0.5f));
        dust.scale -= 0.05f;
        dust.rotation += 0.1f;
        dust.velocity *= 0.92f;
        if (dust.scale <= 0)
        {
            dust.active = false;
        }
        return false;
    }
}
