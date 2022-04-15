using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIElementWrapper<T> : ExxoUIElement where T : UIElement
{
    public readonly T InnerElement;
    private StyleDimension origHeight;
    private StyleDimension origWidth;

    public ExxoUIElementWrapper(T uiElement, bool autoSize = true)
    {
        InnerElement = uiElement;
        if (autoSize)
        {
            InnerElement.Width.Set(0, 1);
            InnerElement.Height.Set(0, 1);
        }

        Append(InnerElement);
    }

    public bool FitToInnerElement { get; set; }
    public bool FitMinToInnerElement { get; set; }

    protected override void PreRecalculate()
    {
        base.PreRecalculate();
        if (FitToInnerElement || FitMinToInnerElement)
        {
            origWidth = Width;
            origHeight = Height;
            Width.Set(0, 1);
            Height.Set(0, 1);
        }
    }

    protected override void PostRecalculate()
    {
        if (FitMinToInnerElement || FitToInnerElement)
        {
            if (FitMinToInnerElement)
            {
                MinWidth.Set(InnerElement.MinWidth.Pixels + PaddingLeft + PaddingRight, 0);
                MinHeight.Set(InnerElement.MinHeight.Pixels + PaddingBottom + PaddingTop, 0);
                Width = origWidth;
                Height = origHeight;
            }

            if (FitToInnerElement)
            {
                Width.Set(InnerElement.GetOuterDimensions().Width + PaddingLeft + PaddingRight, 0);
                Height.Set(InnerElement.GetOuterDimensions().Height + PaddingBottom + PaddingTop, 0);
            }

            RecalculateChildrenSelf();
        }
    }
}
