using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIHerbExchange : ExxoUIPanelWrapper<ExxoUIList>
{
    public readonly ExxoUIElementWrapper<ExxoUIListGrid> Grid;
    public readonly ExxoUIScrollbar Scrollbar;
    public readonly ExxoUIImageButtonToggle Toggle;
    private readonly ExxoUIList list;
    private readonly ExxoUIText title;

    public HerbologyUIHerbExchange() : base(new ExxoUIList())
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

        title = new ExxoUIText("Herb Exchange");
        herbExchangeTitleContainer.Append(title);

        Toggle = new ExxoUIImageButtonToggle(
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed", AssetRequestMode.ImmediateLoad), Color.Red, Color.White)
        {
            Tooltip = "Toggle Seeds/Large Seeds",
        };
        herbExchangeTitleContainer.Append(Toggle);

        var horizontalRule = new ExxoUIHorizontalRule();
        horizontalRule.Width.Set(0, 1);
        list.Append(horizontalRule);

        Grid = new ExxoUIElementWrapper<ExxoUIListGrid>(new ExxoUIListGrid()) { OverflowHidden = true };
        Grid.Width.Set(0, 1);
        Grid.InnerElement.HAlign = UIAlign.Center;
        Grid.InnerElement.FitWidthToContent = true;
        list.Append(Grid, new ExxoUIList.ElementParams(true, false));

        Scrollbar = new ExxoUIScrollbar();
        Scrollbar.VAlign = UIAlign.Center;
        Scrollbar.Height = StyleDimension.Fill;
        Scrollbar.SetPadding(0);
        InnerElement.Append(Scrollbar);
        Grid.InnerElement.ScrollBar = Scrollbar;
        OnScrollWheel += Grid.InnerElement.ScrollWheelListener;
    }
}
