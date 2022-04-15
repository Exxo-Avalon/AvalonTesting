using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIListGrid : ExxoUIList
{
    private readonly int amountPerInnerList;

    public ExxoUIListGrid(int amountPerInnerList = -1)
    {
        this.amountPerInnerList = amountPerInnerList;
        Direction = Direction.Vertical;
        FitHeightToContent = true;
        Width.Set(0, 1);
    }

    public new void Append(UIElement item)
    {
        Append(item, new ElementParams());
    }

    public new void Append(UIElement item, ElementParams elementParams)
    {
        if (Elements.Count == 0)
        {
            AddNewInnerList();
        }

        var lastElement = (Elements[^1] as ExxoUIList)!;

        if (amountPerInnerList == -1)
        {
            if (lastElement.MinWidth.Pixels + item.MinWidth.Pixels + ((lastElement.ElementCount - 1) * ListPadding) >
                GetInnerDimensions().Width)
            {
                lastElement = AddNewInnerList();
            }
        }
        else
        {
            if (lastElement.ElementCount >= amountPerInnerList)
            {
                lastElement = AddNewInnerList();
            }
        }

        lastElement.Append(item, elementParams);
    }

    private ExxoUIList AddNewInnerList()
    {
        var list = new ExxoUIList();
        list.Direction = Direction.Horizontal;
        list.FitHeightToContent = true;
        list.Width.Set(0, 1);
        list.ListPadding = ListPadding;
        base.Append(list);
        return list;
    }
}
