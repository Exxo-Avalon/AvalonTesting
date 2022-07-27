using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUIImageButtonToggle : ExxoUIImageButton
{
    private readonly Color activeColor;

    private readonly Color inactiveColor;
    private bool toggled;

    public ExxoUIImageButtonToggle(Asset<Texture2D> texture, Color inactiveColor, Color activeColor) : base(texture)
    {
        this.inactiveColor = inactiveColor;
        this.activeColor = activeColor;
        OpacityActive = 1.0f;
        OpacityInactive = 0.7f;
    }

    public delegate void ToggleEvent(bool toggled);

    public event ToggleEvent? OnToggle;

    public bool Toggled
    {
        get => toggled;
        set
        {
            toggled = value;
            OnToggle?.Invoke(toggled);
        }
    }

    public override void Click(UIMouseEvent evt)
    {
        base.Click(evt);
        Toggled = !Toggled;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        Color = Toggled ? activeColor : inactiveColor;
        base.DrawSelf(spriteBatch);
    }
}
