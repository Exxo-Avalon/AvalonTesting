using Avalon.Items.Accessories;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Consumables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Items.BossBags;

public class BacteriumPrimeBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Bacterium Prime)");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        SacrificeTotal = 3;
        ItemID.Sets.BossBag[Type] = true;
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
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BacciliteOre>(), 1, 30, 43));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Booger>(), 1, 10, 21));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BadgeOfBacteria>(), 1));
    }*/

    public override void OpenBossBag(Player player)
    {
        //player.TryGettingDevArmor();

        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<BacciliteOre>(), Main.rand.Next(15, 41) + Main.rand.Next(15, 41));
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Booger>(), Main.rand.Next(10, 20));
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<BadgeOfBacteria>());
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.BacteriumPrime>();
}
