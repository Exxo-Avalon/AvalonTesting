using System;
using AvalonTesting.Logic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;

namespace AvalonTesting.UI;

public class ExxoUIScrollbar : UIScrollbar
{
    private readonly Observer<float> observer;
    public EventHandler OnViewPositionChanged;

    public ExxoUIScrollbar()
    {
        observer = new Observer<float>(() => ViewPosition);
    }

    public new void SetView(float viewSize, float maxViewSize)
    {
        viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
        if (viewSize == maxViewSize)
        {
            Width.Set(0, 0);
            if (Parent is ExxoUIElementWrapper exxoParent)
            {
                exxoParent.Hidden = true;
            }
        }
        else
        {
            Width.Set(20, 0);
            if (Parent is ExxoUIElementWrapper exxoParent)
            {
                exxoParent.Hidden = false;
            }
        }

        base.SetView(viewSize, maxViewSize);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (observer.Check())
        {
            OnViewPositionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
