using Avalon.Data;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIPurchaseAttachment : ExxoUIAttachment<ExxoUIItemSlot, ExxoUIPanelWrapper<ExxoUIList>>
{
    public readonly ExxoUIPanelButton<ExxoUIText> Button;
    public readonly ExxoUIPanelWrapper<ExxoUIList> DifferenceContainer;
    public readonly ExxoUINumberInputWithButtons NumberInputWithButtons;
    private readonly ExxoUIImage balanceIcon;
    private readonly ExxoUIList herbCountCostContainer;
    private readonly ExxoUIImage herbTypeIcon;
    private readonly ExxoUIText subBalance;
    private readonly ExxoUIText subHerbCountBalance;

    public HerbologyUIPurchaseAttachment() : base(new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()))
    {
        AttachmentElement.FitMinToInnerElement = true;

        AttachmentElement.InnerElement.FitHeightToContent = true;
        AttachmentElement.InnerElement.FitWidthToContent = true;
        AttachmentElement.InnerElement.ContentHAlign = UIAlign.Center;
        Color newColor = AttachmentElement.BackgroundColor;
        newColor.A = 255;
        AttachmentElement.BackgroundColor = newColor;

        OnPositionAttachment += (sender, e) => e.Position.Y += sender.AttachmentHolder.MinHeight.Pixels;

        NumberInputWithButtons = new ExxoUINumberInputWithButtons();
        AttachmentElement.InnerElement.Append(NumberInputWithButtons);

        DifferenceContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList
        {
            FitHeightToContent = true, FitWidthToContent = true, ListPadding = 10,
        }) { FitMinToInnerElement = true };
        AttachmentElement.InnerElement.Append(DifferenceContainer);

        var tokenCostContainer = new ExxoUIList
        {
            FitHeightToContent = true,
            FitWidthToContent = true,
            ContentVAlign = UIAlign.Center,
            Direction = Direction.Horizontal,
            Justification = Justification.SpaceBetween,
        };
        tokenCostContainer.Width.Set(0, 1);
        DifferenceContainer.InnerElement.Append(tokenCostContainer);

        balanceIcon = new ExxoUIImage(null);
        tokenCostContainer.Append(balanceIcon);

        subBalance = new ExxoUIText("");
        tokenCostContainer.Append(subBalance);

        herbCountCostContainer = new ExxoUIList
        {
            FitHeightToContent = true,
            FitWidthToContent = true,
            ContentVAlign = UIAlign.Center,
            Direction = Direction.Horizontal,
            Justification = Justification.SpaceBetween,
        };
        herbCountCostContainer.Width.Set(0, 1);
        DifferenceContainer.InnerElement.Append(herbCountCostContainer);

        herbTypeIcon = new ExxoUIImage(null);
        herbCountCostContainer.Append(herbTypeIcon);

        subHerbCountBalance = new ExxoUIText("");
        herbCountCostContainer.Append(subHerbCountBalance);

        Button = new ExxoUIPanelButton<ExxoUIText>(new ExxoUIText("Exchange"), false)
        {
            HAlign = UIAlign.Center, FitMinToInnerElement = true,
        };
        Button.Width.Set(0, 1);
        Button.InnerElement.HAlign = UIAlign.Center;

        AttachmentElement.InnerElement.Append(Button);

        OnAttachTo += delegate
        {
            NumberInputWithButtons.NumberInput.Number = 1;
        };
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        Player player = Main.LocalPlayer;
        ExxoHerbologyPlayer modPlayer = player.GetModPlayer<ExxoHerbologyPlayer>();

        int balance;
        bool showHerbCount = HerbologyData.LargeHerbSeedIdByHerbSeedId.ContainsValue(AttachmentHolder.Item.type);
        herbCountCostContainer.Hidden = !showHerbCount;
        if (HerbologyData.ItemIsHerb(AttachmentHolder.Item))
        {
            balance = modPlayer.herbTotal;
            balanceIcon.SetImage(
                Avalon.Mod.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed"));
            balanceIcon.Inset = new Vector2(11, 11);
        }
        else
        {
            balance = modPlayer.potionTotal;
            balanceIcon.SetImage(
                Avalon.Mod.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"));
            balanceIcon.Inset = new Vector2(8, 5);
        }

        int cost = HerbologyData.GetItemCost(AttachmentHolder.Item, NumberInputWithButtons.NumberInput.Number);
        subBalance.SetText($"-{cost}");
        if (balance - cost < 0)
        {
            subBalance.TextColor = Color.Red;
        }
        else
        {
            subBalance.TextColor = Color.White;
        }

        if (showHerbCount)
        {
            int herbType = HerbologyData.GetBaseHerbType(AttachmentHolder.Item);
            if (herbType != -1)
            {
                herbTypeIcon.SetImage(TextureAssets.Item[herbType]);
                subHerbCountBalance.SetText($"-{cost}");
                if (!modPlayer.herbCounts.ContainsKey(herbType) || modPlayer.herbCounts[herbType] - cost < 0)
                {
                    subHerbCountBalance.TextColor = Color.Red;
                }
                else
                {
                    subHerbCountBalance.TextColor = Color.White;
                }
            }
        }
    }
}
