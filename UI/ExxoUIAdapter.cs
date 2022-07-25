using Terraria.UI;

namespace Avalon.UI;

public class ExxoUIAdapter<T> : ExxoUIElement where T : UIElement
{
    private readonly float childBaseMarginBottom;
    private readonly float childBaseMarginLeft;
    private readonly float childBaseMarginRight;
    private readonly float childBaseMarginTop;
    private readonly float childBasePaddingBottom;
    private readonly float childBasePaddingLeft;
    private readonly float childBasePaddingRight;
    private readonly float childBasePaddingTop;

    public ExxoUIAdapter(T childBase)
    {
        ChildBase = childBase;
        childBase.Width = StyleDimension.Fill;
        childBase.Height = StyleDimension.Fill;

        childBasePaddingTop = childBase.PaddingTop;
        childBasePaddingLeft = childBase.PaddingLeft;
        childBasePaddingRight = childBase.PaddingRight;
        childBasePaddingBottom = childBase.PaddingBottom;
        childBaseMarginTop = childBase.MarginTop;
        childBaseMarginLeft = childBase.MarginLeft;
        childBaseMarginRight = childBase.MarginRight;
        childBaseMarginBottom = childBase.MarginBottom;

        childBase.PaddingTop = 0;
        childBase.PaddingLeft = 0;
        childBase.PaddingRight = 0;
        childBase.PaddingBottom = 0;
        childBase.MarginTop = 0;
        childBase.MarginLeft = 0;
        childBase.MarginRight = 0;
        childBase.MarginBottom = 0;

        Append(childBase);
    }

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    protected T ChildBase { get; }

    /// <inheritdoc />
    protected override void PreRecalculate()
    {
        PaddingTop += childBasePaddingTop;
        PaddingLeft += childBasePaddingLeft;
        PaddingRight += childBasePaddingRight;
        PaddingBottom += childBasePaddingBottom;
        MarginTop += childBaseMarginTop;
        MarginLeft += childBaseMarginLeft;
        MarginRight += childBaseMarginRight;
        MarginBottom += childBaseMarginBottom;
    }

    /// <inheritdoc />
    protected override void PostRecalculate()
    {
        PaddingTop -= childBasePaddingTop;
        PaddingLeft -= childBasePaddingLeft;
        PaddingRight -= childBasePaddingRight;
        PaddingBottom -= childBasePaddingBottom;
        MarginTop -= childBaseMarginTop;
        MarginLeft -= childBaseMarginLeft;
        MarginRight -= childBaseMarginRight;
        MarginBottom -= childBaseMarginBottom;
    }
}
