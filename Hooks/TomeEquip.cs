using Terraria.ModLoader;
using Avalon.Common;
using Terraria.ID;
using Terraria;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
internal class TomeEquip : ModHook
{
    protected override void Apply()
    {
        On.Terraria.UI.ItemSlot.SelectEquipPage += OnEquipPage;
    }
    public static void OnEquipPage(On.Terraria.UI.ItemSlot.orig_SelectEquipPage orig, Item item)
    {
        orig(item);
        if (item.type > ItemID.None && item.GetGlobalItem<AvalonGlobalItemInstance>().Tome)
        {
            Main.EquipPage = 0;
        }
    }
}
