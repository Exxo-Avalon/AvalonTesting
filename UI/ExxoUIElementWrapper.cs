using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIElementWrapper : ExxoUIElement
{
    public readonly UIElement InnerElement;
    private StyleDimension origHeight;
    private StyleDimension origWidth;

    public ExxoUIElementWrapper(UIElement uiElement)
    {
        InnerElement = uiElement;
        Append(InnerElement);
    }

    public bool FitMinToInnerElement { get; set; }
    public bool FitToInnerElement { get; set; }

    public override bool IsDynamicallySized => FitMinToInnerElement || FitToInnerElement;

    protected override void PreRecalculate()
    {
        base.PreRecalculate();
        if (FitMinToInnerElement || FitToInnerElement)
        {
            origWidth = Width;
            origHeight = Height;
            Width.Set(0, 1);
            Height.Set(0, 1);
        }
    }

    protected override void PostRecalculate()
    {
        if (FitMinToInnerElement)
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
                Width.Set(InnerElement.MinWidth.Pixels + PaddingLeft + PaddingRight, 0);
                Height.Set(InnerElement.MinHeight.Pixels + PaddingBottom + PaddingTop, 0);
            }

            RecalculateChildrenSelf();
        }
    }
}

public class ExxoUIElementWrapper<T> : ExxoUIElementWrapper where T : UIElement
{
    public ExxoUIElementWrapper(T uiElement) : base(uiElement) { }
    public new T InnerElement => (T)base.InnerElement;
}
