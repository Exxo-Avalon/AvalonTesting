using Microsoft.Xna.Framework;
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
            r.Y = top + 4 * 47;

            Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, r.Top);
            if (Main.mapStyle is 0 or 2)
            {
                p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, 362);
            }
            return p;
        }
    }
    public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
    {
        int top = Hooks.AvalonReflection.Main_mH + 174;
        Rectangle r = new(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));
        r.Y = top + 4 * 47;
        Vector2 p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, r.Top);
        if (Main.mapStyle is 0 or 2)
        {
            p = new Vector2(Main.screenWidth - 64 - 28 - 47 - 47, 362);
        }
        ItemSlot.Draw(Main.spriteBatch, ref item, 10, p);
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
