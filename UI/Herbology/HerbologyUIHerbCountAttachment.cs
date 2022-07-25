using Avalon.Data;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

namespace Avalon.UI.Herbology;

internal class HerbologyUIHerbCountAttachment : ExxoUIAttachment<ExxoUIItemSlot, ExxoUIPanelWrapper<ExxoUIList>>
{
    public readonly ExxoUIImage Image;
    public readonly ExxoUIText Text;

    public HerbologyUIHerbCountAttachment() : base(new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList()))
    {
        AttachmentElement.FitMinToInnerElement = true;
        Color newColor = AttachmentElement.BackgroundColor;
        newColor.A = 255;
        AttachmentElement.BackgroundColor = newColor;

        AttachmentElement.InnerElement.FitHeightToContent = true;
        AttachmentElement.InnerElement.FitWidthToContent = true;
        AttachmentElement.InnerElement.Direction = Direction.Horizontal;

        Image = new ExxoUIImage(null);
        AttachmentElement.InnerElement.Append(Image);

        Text = new ExxoUIText("") { VAlign = UIAlign.Center };
        AttachmentElement.InnerElement.Append(Text);

        OnPositionAttachment += (sender, e) => e.Position.Y -= sender.AttachmentElement.MinHeight.Pixels;
    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        Player player = Main.LocalPlayer;
        ExxoHerbologyPlayer modPlayer = player.GetModPlayer<ExxoHerbologyPlayer>();

        int herbType = HerbologyData.GetBaseHerbType(AttachmentHolder.Item);
        if (herbType != -1)
        {
            if (modPlayer.herbCounts.ContainsKey(herbType))
            {
                Text.SetText(modPlayer.herbCounts[herbType].ToString());
            }
            else
            {
                Text.SetText("0");
            }

            Image.SetImage(TextureAssets.Item[herbType]);
        }
    }
}
