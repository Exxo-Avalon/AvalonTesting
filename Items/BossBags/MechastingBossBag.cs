using AvalonTesting.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.BossBags;

public class MechastingBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 36;
        Item.height = 34;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void OpenBossBag(Player player)
    {
        player.TryGettingDevArmor(player.GetItemSource_OpenItem(Item.type));

        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<SoulofDelight>(), Main.rand.Next(20, 41));
        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Items.Accessories.AIController>(), 1);
        int rn = Main.rand.Next(4);
        if (rn == 0)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Accessories.StingerPack>());
        }
        if (rn == 1)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Weapons.Magic.Mechazapinator>());
        }
        if (rn == 2)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Weapons.Ranged.HeatSeeker>());
        }
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.Mechasting>();
}
