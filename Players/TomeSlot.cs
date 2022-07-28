using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.Players;
public class TomeSlot : ModAccessorySlot
{
    public override bool IsEnabled() => true;
    public override bool IsHidden() => Main.EquipPage != 2;
    public override bool DrawDyeSlot => false;
    public override bool DrawVanitySlot => false;
    public override string FunctionalTexture => "Avalon/Assets/Textures/UI/TomeSlot";
    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => checkItem.GetGlobalItem<AvalonGlobalItemInstance>().Tome;
    public override Vector2? CustomLocation
    {
        get
        {
            int top = Hooks.AvalonReflection.Main_mH + 174;
            Rectangle r = new(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));
            r.Y = top + 0 * 47;

            Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, r.Top);
            if (Main.mapStyle is 0 or 2)
            {
                p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, 362 - 47 * 4);
            }
            return p;
        }
    }
    public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
    {
        int top = Hooks.AvalonReflection.Main_mH + 174;
        Rectangle r = new(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));
        r.Y = top + 0 * 47;
        Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, r.Top);
        if (Main.mapStyle is 0 or 2)
        {
            p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, 362 - 47 * 4);
        }

        int cX = TextureAssets.InventoryBack3.Value.Width / 2;
        int cY = TextureAssets.InventoryBack3.Value.Height / 2;
        int endX = cX;
        int endY = cY;

        if (item.type != 0)
        {
            cX -= TextureAssets.Item[item.type].Value.Width / 2;
            cY -= TextureAssets.Item[item.type].Value.Height / 2;
            endX = TextureAssets.Item[item.type].Value.Width - (cY - cY / 4);
            endY = TextureAssets.Item[item.type].Value.Height - (cY - cY / 4);
        }

        Main.spriteBatch.Draw(TextureAssets.InventoryBack3.Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), Color.White);
        if (item.type == 0)
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(FunctionalTexture).Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), Color.White);
        else
            Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, new Rectangle((int)p.X + cX, (int)p.Y + cY, endX, endY), Color.White);
            //Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, p + new Vector2(thing), null, Color.White, 0f, default, Main.inventoryScale * scaleMod, SpriteEffects.None, 1f);
        return false;
    }
    public override void OnMouseHover(AccessorySlotType context)
    {
        switch (context)
        {
            default:
                Main.hoverItemName = "Tome";
                break;
        }
    }
}
