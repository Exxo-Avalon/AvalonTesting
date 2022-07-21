using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Painting;
using AvalonTesting.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.Items.BossBags;

public class OblivionBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Oblivion)");
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
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CurseofOblivion>(), 1));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AccelerationDrill>(), 1));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofTorture>(), 1, 60, 121));

        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OblivionOre>(), 1, 100, 201));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Accessories.LuckyPapyrus>(), 20));

        LeadingConditionRule r = new LeadingConditionRule((IItemDropRuleCondition)ItemDropRule.ByCondition(new Conditions.NotFromStatue(), ModContent.ItemType<Items.Material.VictoryPiece>(), 5, 1, 1, 4));
        r.OnFailedRoll(ItemDropRule.ByCondition(new Conditions.NotFromStatue(), ModContent.ItemType<Items.Material.VictoryPiece>(), 1, 2), true);
    }*/

    public override void OpenBossBag(Player player)
    {
        player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));

        if (Main.rand.Next(4) == 0)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<CurseofOblivion>(), 1);
        }
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Tools.AccelerationDrill>(), 1);
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<SoulofTorture>(), Main.rand.Next(60, 121));
        if (Main.rand.Next(5) > 0)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<VictoryPiece>(), 1);
        }
        else
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<VictoryPiece>(), 2);
        }
        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<OblivionOre>(), Main.rand.Next(100, 201));
        if (Main.rand.Next(20) == 0)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Accessories.LuckyPapyrus>());
        }
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.CrystalSpectre>(); // CHANGE LATER LMAO
}
