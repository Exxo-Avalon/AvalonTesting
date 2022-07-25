using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIPotionExchange : ExxoUIPanelWrapper<ExxoUIList>
{
    public readonly ExxoUIElementWrapper<ExxoUIListGrid> Grid;
    public readonly ExxoUIElementWrapper<ExxoUIScrollbar> Scrollbar;
    public readonly ExxoUIImageButtonToggle Toggle;
    private readonly ExxoUIList list;
    private readonly ExxoUIText title;

    public HerbologyUIPotionExchange() : base(new ExxoUIList())
    {
        Height.Set(0, 1);
        InnerElement.Direction = Direction.Horizontal;

        list = new ExxoUIList();
        list.Height.Set(0, 1);
        InnerElement.Append(list, new ExxoUIList.ElementParams(true, false));

        var herbExchangeTitleContainer = new ExxoUIList { FitHeightToContent = true, ContentVAlign = UIAlign.Center };
        herbExchangeTitleContainer.Width.Set(0, 1);
        herbExchangeTitleContainer.Direction = Direction.Horizontal;
        herbExchangeTitleContainer.Justification = Justification.Center;
        list.Append(herbExchangeTitleContainer);

        title = new ExxoUIText("Potion Exchange");
        herbExchangeTitleContainer.Append(title);

        Toggle = new ExxoUIImageButtonToggle(
            Avalon.Mod.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"), Color.White,
            Color.Orange) { Tooltip = "Toggle Potions/Elixirs" };
        herbExchangeTitleContainer.Append(Toggle);

        var horizontalRule = new ExxoUIHorizontalRule();
        horizontalRule.Width.Set(0, 1);
        list.Append(horizontalRule);

        Grid = new ExxoUIElementWrapper<ExxoUIListGrid>(new ExxoUIListGrid()) { OverflowHidden = true };
        Grid.Width.Set(0, 1);
        Grid.InnerElement.HAlign = UIAlign.Center;
        Grid.InnerElement.FitWidthToContent = true;
        list.Append(Grid, new ExxoUIList.ElementParams(true, false));

        Scrollbar = new ExxoUIElementWrapper<ExxoUIScrollbar>(new ExxoUIScrollbar()) { FitToInnerElement = true };
        Scrollbar.VAlign = UIAlign.Center;
        Scrollbar.SetPadding(0);
        InnerElement.Append(Scrollbar);
        Grid.InnerElement.ScrollBar = Scrollbar.InnerElement;
        OnScrollWheel += Grid.InnerElement.ScrollWheelListener;
    }
}
