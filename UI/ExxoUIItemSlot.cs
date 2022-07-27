using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUIItemSlot : ExxoUIImageButton
{
    public readonly ExxoUIImage InnerImage;
    public bool HoverItemDrawStack = true;
    private Item item;

    public ExxoUIItemSlot(Asset<Texture2D> backgroundTexture, int itemID) : base(backgroundTexture)
    {
        item = new Item();
        item.netDefaults(itemID);
        item.stack = 1;

        InnerImage = new ExxoUIImage(TextureAssets.Item[item.type])
        {
            HAlign = UIAlign.Center, VAlign = UIAlign.Center,
        };
        Append(InnerImage);
    }

    public Item Item
    {
        get => item;
        set
        {
            item = value;
            InnerImage.SetImage(TextureAssets.Item[Item.type]);
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        if (IsMouseHovering)
        {
            Main.hoverItemName = " ";
            Main.HoverItem = Item;
        }
    }

    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        if (Item.stack > 0)
        {
            base.DrawChildren(spriteBatch);
        }
    }
}
