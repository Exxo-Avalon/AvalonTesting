using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIGroupOptionButton<T> : ExxoUIElement where T : IComparable
{
    private const float WhiteLerp = 0.7f;
    private readonly Asset<Texture2D> basePanelTexture;

    public readonly LocalizedText Description;
    private readonly Asset<Texture2D> hoveredBorderTexture;
    private readonly ExxoUIList mainElement;
    public readonly T OptionValue;
    private readonly Asset<Texture2D> selectedBorderTexture;
    private T currentGroupOption;
    public float FadeFromBlack = 1f;
    public bool ShowHighlightWhenSelected = true;

    public ExxoUIGroupOptionButton(T option, LocalizedText title, LocalizedText description, Color textColor,
                                   Asset<Texture2D> iconTexture, float textSize = 1f, float titleAlignmentX = 0.5f,
                                   float titleWidthReduction = 10f)
    {
        BorderColor = Color.White;
        currentGroupOption = option;
        OptionValue = option;
        Description = description;
        Width = StyleDimension.FromPixels(44f);
        Height = StyleDimension.FromPixels(34f);
        basePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
        selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
        hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");

        SelectedColor = Colors.InventoryDefaultColor;
        UnselectedColor = SelectedColor;

        mainElement = new ExxoUIList();
        mainElement.Direction = Direction.Horizontal;
        mainElement.Height.Set(0, 1);
        mainElement.Width.Set(32, 0);
        mainElement.ContentVAlign = 0.5f;
        mainElement.ContentHAlign = 0.5f;

        Append(mainElement);
        if (iconTexture != null)
        {
            var uiImage = new ExxoUIImage(iconTexture);
            mainElement.Append(uiImage);
        }

        if (title != null)
        {
            var uIText = new ExxoUIText(title, textSize)
            {
                HAlign = titleAlignmentX,
                VAlign = 0.5f,
                Width = StyleDimension.FromPixelsAndPercent(0f - titleWidthReduction, 1f),
                Top = StyleDimension.FromPixels(0f),
                TextColor = textColor
            };
            Append(uIText);
        }
        else
        {
            mainElement.Width.Set(0, 1);
        }
    }

    public Color BorderColor { get; set; }
    public float SelectedOpacity { get; set; } = 0.7f;
    public float UnselectedOpacity { get; set; } = 0.7f;
    public Color SelectedColor { get; set; }
    public Color UnselectedColor { get; set; }

    public override bool IsDynamicallySized => false;

    public bool IsSelected => currentGroupOption.Equals(OptionValue);

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        CalculatedStyle dimensions = GetDimensions();
        Color color = IsSelected ? SelectedColor : UnselectedColor;
        float scale = IsSelected ? SelectedOpacity : UnselectedOpacity;

        Utils.DrawSplicedPanel(spriteBatch, basePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y,
            (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10,
            Color.Lerp(Color.Black, color, FadeFromBlack) * scale);
        if (IsSelected && ShowHighlightWhenSelected)
        {
            Utils.DrawSplicedPanel(spriteBatch, selectedBorderTexture.Value, (int)dimensions.X + 7,
                (int)dimensions.Y + 7, (int)dimensions.Width - 14, (int)dimensions.Height - 14, 10, 10, 10, 10,
                Color.Lerp(color, Color.White, WhiteLerp) * scale);
        }

        if (IsMouseHovering)
        {
            Utils.DrawSplicedPanel(spriteBatch, hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y,
                (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, BorderColor);
        }
        //
        // if (iconTexture != null)
        // {
        //     Color color2 = Color.White;
        //     if (!IsMouseHovering && !IsSelected)
        //     {
        //         color2 = Color.Lerp(color, Color.White, WhiteLerp) * scale;
        //     }
        //
        //     spriteBatch.Draw(iconTexture.Value, new Vector2(dimensions.X + 1f, dimensions.Y + 1f), color2);
        // }
    }

    public void SetCurrentOption(T option)
    {
        currentGroupOption = option;
    }

    protected override void FirstMouseOver(UIMouseEvent evt)
    {
        base.FirstMouseOver(evt);
        SoundEngine.PlaySound(SoundID.MenuTick);
    }

    public override void MouseDown(UIMouseEvent evt)
    {
        SoundEngine.PlaySound(12);
        base.MouseDown(evt);
    }
}
