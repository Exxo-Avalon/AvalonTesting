﻿using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIStats : ExxoUIPanelWrapper<ExxoUIList>
{
    public readonly ExxoUITextPanel HerbTierText;
    public readonly ExxoUIPanelWrapper<ExxoUIList> HerbTotalContainer;
    public readonly ExxoUIPanelWrapper<ExxoUIList> PotionTotalContainer;
    public readonly ExxoUITextPanel RankTitleText;
    private readonly ExxoUIImage herbTotalIcon;
    private readonly ExxoUIText herbTotalText;
    private readonly ExxoUIImage potionTotalIcon;
    private readonly ExxoUIText potionTotalText;

    public HerbologyUIStats() : base(new ExxoUIList())
    {
        FitMinToInnerElement = true;
        Height.Set(0, 1);

        InnerElement.FitWidthToContent = true;
        InnerElement.Justification = Justification.Center;
        InnerElement.ContentHAlign = UIAlign.Center;

        RankTitleText = new ExxoUITextPanel("");
        RankTitleText.TextElement.TextColor = Color.Gold;
        InnerElement.Append(RankTitleText);

        HerbTierText = new ExxoUITextPanel("");
        InnerElement.Append(HerbTierText);

        HerbTotalContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList())
        {
            FitMinToInnerElement = true, Tooltip = "Herb credits",
        };
        HerbTotalContainer.InnerElement.Direction = Direction.Horizontal;
        HerbTotalContainer.InnerElement.FitHeightToContent = true;
        HerbTotalContainer.InnerElement.FitWidthToContent = true;
        HerbTotalContainer.InnerElement.ContentVAlign = UIAlign.Center;
        InnerElement.Append(HerbTotalContainer);

        herbTotalIcon =
            new ExxoUIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed", AssetRequestMode.ImmediateLoad))
            {
                Inset = new Vector2(7, 7),
            };
        HerbTotalContainer.InnerElement.Append(herbTotalIcon);

        herbTotalText = new ExxoUIText("");
        HerbTotalContainer.InnerElement.Append(herbTotalText);

        PotionTotalContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList())
        {
            FitMinToInnerElement = true, Tooltip = "Potion credits",
        };
        PotionTotalContainer.InnerElement.Direction = Direction.Horizontal;
        PotionTotalContainer.InnerElement.FitHeightToContent = true;
        PotionTotalContainer.InnerElement.FitWidthToContent = true;
        PotionTotalContainer.InnerElement.ContentVAlign = UIAlign.Center;
        InnerElement.Append(PotionTotalContainer);

        potionTotalIcon =
            new ExxoUIImage(Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption", AssetRequestMode.ImmediateLoad))
            {
                Inset = new Vector2(4, 5),
            };
        PotionTotalContainer.InnerElement.Append(potionTotalIcon);

        potionTotalText = new ExxoUIText("");
        PotionTotalContainer.InnerElement.Append(potionTotalText);
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        Player player = Main.LocalPlayer;
        ExxoHerbologyPlayer modPlayer = player.GetModPlayer<ExxoHerbologyPlayer>();

        string rankTitle = $"Herbology {modPlayer.Tier}";
        RankTitleText.TextElement.SetText(rankTitle);

        string tier = $"Tier {(int)modPlayer.Tier + 1} Herbologist";
        HerbTierText.TextElement.SetText(tier);

        string herbTotal = modPlayer.HerbTotal.ToString();
        herbTotalText.SetText(herbTotal);

        string potionTotal = modPlayer.PotionTotal.ToString();
        potionTotalText.SetText(potionTotal);
    }
}