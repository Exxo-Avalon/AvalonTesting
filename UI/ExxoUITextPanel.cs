using Terraria.Localization;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUITextPanel : ExxoUIPanel
{
    public ExxoUITextPanel(string text, float textScale = 1f, bool large = false) : this(new ExxoUIText(text, textScale,
        large))
    {
    }

    public ExxoUITextPanel(LocalizedText text, float textScale = 1f, bool large = false) : this(new ExxoUIText(text,
        textScale, large))
    {
    }

    protected ExxoUITextPanel(ExxoUIText textElement)
    {
        TextElement = textElement;
        TextElement.VAlign = UIAlign.Center;
        TextElement.HAlign = UIAlign.Center;
        Append(TextElement);
        TextElement.OnInternalTextChange += () =>
        {
            MinWidth.Pixels = TextElement.MinWidth.Pixels + PaddingLeft + PaddingRight;
            MinHeight.Pixels = TextElement.MinHeight.Pixels + PaddingBottom + PaddingTop;
        };
    }

    public ExxoUIText TextElement { get; }
}
