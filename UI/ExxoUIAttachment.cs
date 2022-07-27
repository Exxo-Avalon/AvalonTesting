using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.UI;

internal class PositionAttachmentEventArgs : EventArgs
{
    public Vector2 Position;

    public PositionAttachmentEventArgs(Vector2 position) => Position = position;
}

internal class ExxoUIAttachment<THolder, TAttachment> : ExxoUIElement
    where THolder : ExxoUIElement where TAttachment : ExxoUIElement
{
    public readonly TAttachment AttachmentElement;

    public ExxoUIAttachment(TAttachment uiElement)
    {
        Active = false;
        Width.Set(0, 1);
        Height.Set(0, 1);
        AttachmentElement = uiElement;
        Append(AttachmentElement);
    }

    public delegate void ExxoUIAttachmentEventHandler(ExxoUIAttachment<THolder, TAttachment> sender, EventArgs e);

    public delegate void PositionAttachmentEventHandler(ExxoUIAttachment<THolder, TAttachment> sender,
                                                        PositionAttachmentEventArgs e);

    public event ExxoUIAttachmentEventHandler OnAttachTo;
    public event PositionAttachmentEventHandler OnPositionAttachment;

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    public THolder AttachmentHolder { get; private set; }

    public override bool ContainsPoint(Vector2 point) => IsVisible && AttachmentElement.ContainsPoint(point);

    public virtual void AttachTo(THolder attachmentHolder)
    {
        AttachmentHolder = attachmentHolder;
        if (AttachmentHolder == null)
        {
            Active = false;
        }
        else
        {
            Active = true;
            OnAttachTo?.Invoke(this, EventArgs.Empty);
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        Vector2 position = AttachmentHolder.GetDimensions().Position() - Parent.GetOuterDimensions().Position();
        var args = new PositionAttachmentEventArgs(position);
        OnPositionAttachment?.Invoke(this, args);
        AttachmentElement.Left.Set(args.Position.X, 0);
        AttachmentElement.Top.Set(args.Position.Y, 0);
        RecalculateChildrenSelf();
        base.DrawSelf(spriteBatch);
    }
}
