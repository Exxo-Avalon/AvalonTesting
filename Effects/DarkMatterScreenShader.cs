using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace AvalonTesting;
public class DarkMatterScreenShader : ScreenShaderData
{
    public DarkMatterScreenShader(Ref<Effect> shader, string passName) : base(shader, passName)
    {
    }

    public override void Apply()
    {
        var vec = new Color(126, 71, 107).ToVector3(); // 126 71 107
        vec *= 0.4f;
        UseOpacity(Math.Max(vec.X, Math.Max(vec.Y, vec.Z)));
        base.Apply();
    }

    public override void Update(GameTime gameTime)
    {
        if (!Main.LocalPlayer.GetModPlayer<Players.ExxoBiomePlayer>().ZoneDarkMatter)
        {
            Filters.Scene["AvalonTesting:DarkMatter"].Deactivate();
        }
    }
}
