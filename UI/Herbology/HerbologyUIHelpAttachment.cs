using Microsoft.Xna.Framework;

namespace Avalon.UI.Herbology;

internal class HerbologyUIHelpAttachment : ExxoUIAttachment<ExxoUIElement, ExxoUITextPanel>
{
    private bool attachedThisUpdate;
    private bool enabled;

    public HerbologyUIHelpAttachment() : base(new ExxoUITextPanel(""))
    {
        Color newColor = AttachmentElement.BackgroundColor;
        newColor.A = 255;
        AttachmentElement.BackgroundColor = newColor;

        OnPositionAttachment += (sender, e) => e.Position.Y -= sender.AttachmentElement.GetOuterDimensions().Height;
    }

    public bool Enabled
    {
        get => enabled;
        set
        {
            enabled = value;
            if (!enabled)
            {
                AttachTo(null);
            }
        }
    }

    public override void AttachTo(ExxoUIElement attachmentHolder)
    {
        if (attachmentHolder != AttachmentHolder)
        {
            if (AttachmentHolder is ExxoUIPanel panel)
            {
                panel.BorderColor = ExxoUIPanel.DefaultBorderColor;
            }
        }

        base.AttachTo(attachmentHolder);
        if (AttachmentHolder != null)
        {
            if (AttachmentHolder is ExxoUIPanel panel)
            {
                panel.BorderColor = Color.Gold;
            }
        }
    }

    public void Register(ExxoUIElement element, string description)
    {
        element.OnMouseOver += (evt, _) =>
        {
            if (!attachedThisUpdate && Enabled && element.ContainsPoint(evt.MousePosition))
            {
                attachedThisUpdate = true;
                AttachTo(element);
                AttachmentElement.TextElement.SetText(description);
            }
        };
        element.OnLastMouseOut += delegate
        {
            if (AttachmentHolder == element)
            {
                AttachTo(null);
            }
        };
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);
        attachedThisUpdate = false;
    }
}
