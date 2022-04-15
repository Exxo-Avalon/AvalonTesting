using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace AvalonTesting.UI;

public class ExxoUIText : ExxoUIElement
{
    public bool DynamicallyScaleDownToWidth;
    private bool isLarge;
    private bool isWrapped;
    private string text = "";
    private float textScale;
    private Vector2 textSize = Vector2.Zero;
    private string visibileText;

    public ExxoUIText(string text, float textScale = 1f, bool isLarge = false)
    {
        TextOriginX = 0.5f;
        TextOriginY = 0.0f;
        WrappedTextBottomPadding = 20f;
        isWrapped = false;
        SetText(text, textScale, isLarge);
    }

    public ExxoUIText(LocalizedText text, float textScale = 1f, bool isLarge = false) : this(text.ToString(), textScale,
        isLarge)
    {
    }

    public float TextOriginX { get; set; }
    public float TextOriginY { get; set; }
    public float WrappedTextBottomPadding { get; set; }

    public override bool IsDynamicallySized => false;

    public string Text
    {
        get => text;
        set
        {
            text = value;
            UpdateText();
        }
    }

    public float TextScale
    {
        get => textScale;
        set
        {
            textScale = value;
            UpdateText();
        }
    }

    public bool IsLarge
    {
        get => isLarge;
        set
        {
            isLarge = value;
            UpdateText();
        }
    }

    public bool IsWrapped
    {
        get => isWrapped;
        set
        {
            isWrapped = value;
            UpdateText();
        }
    }

    public Color TextColor { get; set; } = Color.White;

    public void SetText(string text, float textScale = 1f, bool isLarge = false)
    {
        this.text = text;
        this.textScale = textScale;
        this.isLarge = isLarge;
        UpdateText();
    }

    private void UpdateText()
    {
        DynamicSpriteFont dynamicSpriteFont = IsLarge ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;
        visibileText = !IsWrapped
            ? Text
            : dynamicSpriteFont.CreateWrappedText(Text, GetInnerDimensions().Width / TextScale);
        Vector2 vector2_1 = dynamicSpriteFont.MeasureString(visibileText);
        Vector2 vector2_2 = textSize = !IsWrapped
            ? new Vector2(vector2_1.X, IsLarge ? 32f : 16f) * textScale
            : new Vector2(vector2_1.X, vector2_1.Y + WrappedTextBottomPadding) * textScale;
        MinWidth.Set(vector2_2.X + PaddingLeft + PaddingRight, 0.0f);
        MinHeight.Set(vector2_2.Y + PaddingTop + PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.AnisotropicClamp, DepthStencilState.None, null,
            null, Main.UIScaleMatrix);

        CalculatedStyle innerDimensions = GetInnerDimensions();
        Vector2 pos = innerDimensions.Position();
        if (IsLarge)
        {
            pos.Y -= 10f * TextScale;
        }
        else
        {
            pos.Y -= 2f * TextScale;
        }

        pos.X += (innerDimensions.Width - textSize.X) * TextOriginX;
        pos.Y += (innerDimensions.Height - textSize.Y) * TextOriginY;

        float scale = textScale;
        if (DynamicallyScaleDownToWidth && textSize.X > (double)innerDimensions.Width)
        {
            scale *= innerDimensions.Width / textSize.X;
        }

        if (IsLarge)
        {
            Utils.DrawBorderStringBig(spriteBatch, visibileText, pos, TextColor, scale);
        }
        else
        {
            Utils.DrawBorderString(spriteBatch, visibileText, pos, TextColor, scale);
        }

        spriteBatch.End();
        BeginDefaultSpriteBatch(spriteBatch);
    }
}
