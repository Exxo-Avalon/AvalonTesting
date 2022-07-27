using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIDraggablePanel : ExxoUIPanel
{
    private Vector2 clickDelta;
    private bool isMouseHeld;

    public override void MouseDown(UIMouseEvent evt)
    {
        if (evt.Target == ChildBase)
        {
            clickDelta = UserInterface.ActiveInstance.MousePosition -
                         new Vector2(GetInnerDimensions().X, GetInnerDimensions().Y);
            isMouseHeld = true;
        }

        base.MouseDown(evt);
    }

    public override void MouseUp(UIMouseEvent evt)
    {
        isMouseHeld = false;
        base.MouseUp(evt);
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        if (isMouseHeld)
        {
            Vector2 mouseDelta = UserInterface.ActiveInstance.MousePosition -
                                 (new Vector2(GetInnerDimensions().X, GetInnerDimensions().Y) + clickDelta);
            Left.Set(Left.Pixels + mouseDelta.X, 0);
            Top.Set(Top.Pixels + mouseDelta.Y, 0);
        }
    }
}
