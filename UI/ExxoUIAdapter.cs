using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIAdapter<T> : ExxoUIElement where T : UIElement
{
    protected ExxoUIAdapter(T childBase)
    {
        ChildBase = childBase;
        childBase.Width = StyleDimension.Fill;
        childBase.Height = StyleDimension.Fill;

        PaddingTop = childBase.PaddingTop;
        PaddingLeft = childBase.PaddingLeft;
        PaddingRight = childBase.PaddingRight;
        PaddingBottom = childBase.PaddingBottom;
    }

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    protected T ChildBase { get; }

    /// <inheritdoc />
    protected override void PreRecalculate()
    {
        float oldPaddingTop = PaddingTop;
        float oldPaddingBottom = PaddingBottom;
        float oldPaddingRight = PaddingRight;
        float oldPaddingLeft = PaddingLeft;
        PaddingTop = 0;
        PaddingLeft = 0;
        PaddingRight = 0;
        PaddingBottom = 0;
        RecalculateSelf();
        Append(ChildBase);
        RemoveChild(ChildBase);
        PaddingTop = oldPaddingTop;
        PaddingLeft = oldPaddingLeft;
        PaddingRight = oldPaddingRight;
        PaddingBottom = oldPaddingBottom;
    }

    /// <inheritdoc />
    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        ChildBase.Draw(spriteBatch);
        base.DrawChildren(spriteBatch);
    }

    /// <inheritdoc />
    protected override void UpdateSelf(GameTime gameTime)
    {
        ChildBase.Update(gameTime);
        base.UpdateSelf(gameTime);
    }
}
