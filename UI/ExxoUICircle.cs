using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace AvalonTesting.UI;

public class ExxoUICircle : ExxoUIElement
{
    private readonly Asset<Effect> circleEffect =
        AvalonTesting.Mod.Assets.Request<Effect>("Effects/Circle", AssetRequestMode.ImmediateLoad);

    public override bool IsDynamicallySized => false;
    public Color Color { get; set; } = Color.White;

    public override bool ContainsPoint(Vector2 point) =>
        Vector2.Distance(GetDimensions().Center(), point) <= GetDimensions().Width / 2;

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        spriteBatch.End();
        circleEffect.Value.CurrentTechnique = circleEffect.Value.Techniques["Default"];

        using var whiteRectangle = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
        whiteRectangle.SetData(new[] { Color.White });

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
            DepthStencilState.None,
            RasterizerState.CullNone, circleEffect.Value, Main.UIScaleMatrix);

        circleEffect.Value.Parameters["Color"].SetValue(Color.ToVector4());

        spriteBatch.Draw(
            whiteRectangle,
            GetDimensions().ToRectangle(),
            Color.White);
        spriteBatch.End();
        BeginDefaultSpriteBatch(spriteBatch);
    }
}
