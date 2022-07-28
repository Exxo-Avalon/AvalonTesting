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
        float scaleMod = 1f;
        float thing = 7;
        if (item.type != 0)
        {
            if (TextureAssets.Item[item.type].Value.Width * TextureAssets.Item[item.type].Value.Height > 1280)
            {
                scaleMod = 0.85f;
                thing = 9;
            }
        }
        Main.spriteBatch.Draw(TextureAssets.InventoryBack3.Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), Color.White);
        if (item.type == 0)
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(FunctionalTexture).Value, new Rectangle((int)p.X, (int)p.Y, (int)(52 * Main.inventoryScale), (int)(52 * Main.inventoryScale)), Color.White);
        else
            Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, p + new Vector2(thing), null, Color.White, 0f, default, Main.inventoryScale * scaleMod, SpriteEffects.None, 1f);
            //Main.spriteBatch.Draw(TextureAssets.Item[item.type].Value, new Rectangle((int)p.X + 5, (int)p.Y + 5, (int)(TextureAssets.Item[item.type].Value.Width * Main.inventoryScale) - 10, (int)(TextureAssets.Item[item.type].Value.Height * Main.inventoryScale)), Color.White);
        //ItemSlot.Draw(Main.spriteBatch, ref item, 10, p);
        return false;
    }
    //public override void ApplyEquipEffects()
    //{

    //}
    public override void OnMouseHover(AccessorySlotType context)
    {
        // We will modify the hover text while an item is not in the slot, so that it says "Wings".
        switch (context)
        {
            default:
                Main.hoverItemName = "Tome";
                break;
        }
    }
}
