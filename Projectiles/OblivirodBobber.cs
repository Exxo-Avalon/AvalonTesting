using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Projectiles;

public class OblivirodBobber : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Oblivirod Bobber");
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BobberWooden);

        DrawOriginOffsetY = -8; // Adjusts the draw position
    }

    public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
    {
        lineOriginOffset = new Vector2(45, -35);
        lineColor = new Color(234, 113, 201, 100);
    }
}
