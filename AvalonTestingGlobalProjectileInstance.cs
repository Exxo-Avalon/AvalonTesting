using System;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AvalonTesting;
public class AvalonTestingGlobalProjectileInstance : GlobalProjectile
{
    public Vector2 RotateAboutOrigin(Vector2 point, float rotation)
    {
        if (rotation < 0f)
        {
            rotation += 12.566371f;
        }
        var value = point;
        if (value == Vector2.Zero)
        {
            return point;
        }
        var num = (float)Math.Atan2(value.Y, value.X);
        num += rotation;
        return value.Length() * new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
    }
}
