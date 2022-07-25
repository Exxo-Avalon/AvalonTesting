using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace Avalon.UI;

public abstract class ExxoUIState : UIState
{
    private bool mouseWasOver;

    private int oldFocusRecipe;
    protected virtual bool DisableRecipeScrolling => true;
    protected virtual bool FocusInteractionsToUI => true;
    protected virtual bool HideItemHoverIcon => true;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (mouseWasOver)
        {
            Main.LocalPlayer.cursorItemIconEnabled = !HideItemHoverIcon;
            Main.LocalPlayer.mouseInterface = FocusInteractionsToUI;
            if (DisableRecipeScrolling)
            {
                Main.focusRecipe = oldFocusRecipe;
            }
        }
    }

    public override void OnInitialize()
    {
        base.OnInitialize();
        RemoveAllChildren();
    }

    public override void MiddleDoubleClick(UIMouseEvent evt)
    {
        base.MiddleDoubleClick(evt);
        OnInitialize(); //TODO: REMOVE
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);
        if (evt.Target != this && !mouseWasOver)
        {
            mouseWasOver = true;
            oldFocusRecipe = Main.focusRecipe;
        }
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);
        if (!ChildrenContainsPoint(evt.MousePosition))
        {
            mouseWasOver = false;
        }
    }


    public bool ChildrenContainsPoint(Vector2 point) => Elements.Any(element => element.ContainsPoint(point));
}
