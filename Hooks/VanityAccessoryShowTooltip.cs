using Terraria.ModLoader;
using Avalon.Common;
using Terraria.ID;
using Terraria;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
internal class VanityAccessoryShowTooltip : ModHook
{
    protected override void Apply()
    {
        On.Terraria.Main.MouseText_DrawItemTooltip_GetLinesInfo += OnMouseText_DrawItemTooltip_GetLinesInfo;
    }
    public static void OnMouseText_DrawItemTooltip_GetLinesInfo(On.Terraria.Main.orig_MouseText_DrawItemTooltip_GetLinesInfo orig, Item item, ref int yoyoLogo, ref int researchLine, float oldKB, ref int numLines, string[] toolTipLine, bool[] preFixLine, bool[] badPreFixLine, string[] toolTipNames)
    {
        if (item.social && (item.type == ItemID.HighTestFishingLine || item.type == ModContent.ItemType<Items.Accessories.ShadowRing>() ||
            item.type == ModContent.ItemType<Items.Accessories.ShadowCharm>() || item.type == ModContent.ItemType<Items.Accessories.ShadowPulse>() ||
            item.type == ModContent.ItemType<Items.Accessories.ShadowPulseBag>() || item.type == ModContent.ItemType<Items.Accessories.BagofBlood>() ||
            item.type == ModContent.ItemType<Items.Accessories.BagofFire>() || item.type == ModContent.ItemType<Items.Accessories.BagofFrost>() ||
            item.type == ModContent.ItemType<Items.Accessories.BagofHallows>() || item.type == ModContent.ItemType<Items.Accessories.BagofIck>() ||
            item.type == ModContent.ItemType<Items.Accessories.BagofShadows>() || item.type == ModContent.ItemType<Items.Accessories.Omnibag>()))
        {
            item.social = false;
        }
        orig(item, ref yoyoLogo, ref researchLine, oldKB, ref numLines, toolTipLine, preFixLine, badPreFixLine, toolTipNames);
    }
}
