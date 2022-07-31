using Avalon.Items.Accessories;
using Avalon.Items.Consumables;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Magic;
using Avalon.Items.Weapons.Ranged;
using Avalon.NPCs.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Items.BossBags;

public class WallofSteelBossBag : ModItem
{
    public override int BossBagNPC => ModContent.NPCType<WallofSteel>();

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Wall of Steel)");
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

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.ByCondition(new DropConditions.NotUsedMechHeart(), ModContent.ItemType<MechanicalHeart>()));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulofBlight>(), 1, 40, 56));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellsteelPlate>(), 1, 20, 31));

        itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<FleshBoiler>(),
            ModContent.ItemType<MagicCleaver>(), ModContent.ItemType<BubbleBoost>() }));
    }

    //public override void OpenBossBag(Player player)
    //{
    //    player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));

    //    if ((player.extraAccessorySlots == 0 && !Main.expertMode) ||
    //        (player.extraAccessorySlots == 1 && Main.expertMode))
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<MechanicalHeart>());
    //    }

    //    int drop = Main.rand.Next(5);
    //    if (drop == 0)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<FleshBoiler>());
    //    }

    //    if (drop == 1)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<MagicCleaver>());
    //    }

    //    if (drop == 2)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<BubbleBoost>());
    //    }

    //    player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<SoulofBlight>(),
    //        Main.rand.Next(40, 56));
    //    player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<HellsteelPlate>(),
    //        Main.rand.Next(20, 31));
    //}
}
