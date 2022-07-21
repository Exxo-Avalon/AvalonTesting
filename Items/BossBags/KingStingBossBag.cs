using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.Items.BossBags;

public class KingStingBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (King Sting)");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        ItemID.Sets.BossBag[Type] = true;
        SacrificeTotal = 3;
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

    /*public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ItemID.BeeWax, 1, 16, 27));
        itemLoot.Add(ItemDropRule.Common(ItemID.BottledHoney, 1, 5, 16));
        itemLoot.Add(ItemDropRule.Common(ItemID.JestersArrow, 4, 20, 31));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vanity.KingStingMask>(), 7));
    }*/

    public override void OpenBossBag(Player player)
    {
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.BeeWax, Main.rand.Next(16, 27));
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.BottledHoney, Main.rand.Next(5, 16));
        if (Main.rand.Next(4) == 0) player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.JestersArrow, Main.rand.Next(20, 31));
        if (Main.rand.Next(7) == 0) player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Vanity.KingStingMask>());
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.KingSting>();
}
