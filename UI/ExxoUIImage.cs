using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace Avalon.UI;

public class ExxoUIImage : ExxoUIElement
{
    public float LocalRotation;
    public float LocalScale = 1f;
    protected Color Color = Color.White;
    private Vector2 inset;
    private float scale = 1f;

    public ExxoUIImage(Asset<Texture2D> texture)
    {
        OverrideSamplerState = SamplerState.PointClamp;
        SetImage(texture);
    }

    public override bool IsDynamicallySized => false;

    public Vector2 Inset
    {
        get => inset;
        set
        {
            inset = value;
            UpdateDimensions();
        }
    }

    public float Scale
    {
        get => scale;
        set
        {
            scale = value;
            UpdateDimensions();
        }
    }

    protected Asset<Texture2D>? Texture { get; private set; }

    public void SetImage(Asset<Texture2D>? texture)
    {
        texture?.VanillaLoad();
        Texture = texture;
        UpdateDimensions();
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            spriteBatch.Draw(Texture.Value,
                (GetDimensions().Position() + (Texture.Size() * Scale / 2) - (Inset * Scale)).ToNearestPixel(), null,
                Color, LocalRotation, Texture.Size() / 2, Scale * LocalScale, SpriteEffects.None, 0f);
        }
    }

    private void UpdateDimensions()
    {
        if (Texture == null)
        {
            return;
        }

        MinWidth.Set((Texture.Width() - (Inset.X * 2)) * Scale, 0f);
        MinHeight.Set((Texture.Height() - (Inset.Y * 2)) * Scale, 0f);
    }
}
