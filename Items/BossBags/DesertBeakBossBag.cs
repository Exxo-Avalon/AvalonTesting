using AvalonTesting.Items.Material;
using AvalonTesting.Items.Weapons.Magic;
using AvalonTesting.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.Items.BossBags;

public class DesertBeakBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Desert Beak)");
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
        itemLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 22, 55));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertFeather>(), 1, 18, 24));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TomeoftheDistantPast>(), 3));
        // ore drop
    }*/

    public override void OpenBossBag(Player player)
    {
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.SandBlock, Main.rand.Next(22, 55));
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<DesertFeather>(), Main.rand.Next(18, 24));
        if (ModContent.GetInstance<ExxoWorldGen>().RhodiumOre == ExxoWorldGen.RhodiumVariant.Rhodium)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Placeable.Tile.RhodiumOre>(), Main.rand.Next(40, 61));
        }
        else if (ModContent.GetInstance<ExxoWorldGen>().RhodiumOre == ExxoWorldGen.RhodiumVariant.Osmium)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Placeable.Tile.OsmiumOre>(), Main.rand.Next(40, 61));
        }
        else if (ModContent.GetInstance<ExxoWorldGen>().RhodiumOre == ExxoWorldGen.RhodiumVariant.Iridium)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Placeable.Tile.IridiumOre>(), Main.rand.Next(40, 61));
        }
        if (Main.rand.Next(3) == 0)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<TomeoftheDistantPast>(), 1);
        }
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.DesertBeak>();
}
