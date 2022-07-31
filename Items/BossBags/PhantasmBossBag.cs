using Avalon.Items.Accessories;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Items.BossBags;

public class PhantasmBossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Treasure Bag (Phantasm)");
        Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        ItemID.Sets.BossBag[Type] = true;
        SacrificeTotal = 3;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstrallineArtifact>(), 1));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GhostintheMachine>(), 1, 4, 6));

        itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<VampireTeeth>(),
            ModContent.ItemType<PhantomKnives>(), ModContent.ItemType<EtherealHeart>() }));
    }

    //public override void OpenBossBag(Player player)
    //{
    //    player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
    //    player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<Items.Accessories.AstrallineArtifact>());
    //    int drop = Main.rand.Next(3);
    //    if (drop == 0)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<VampireTeeth>());
    //    }
    //    else if (drop == 1)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<PhantomKnives>());
    //    }
    //    else if (drop == 2)
    //    {
    //        player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<EtherealHeart>());
    //    }
    //    player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ModContent.ItemType<GhostintheMachine>(), Main.rand.Next(3, 6));
    //}

    //public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.Phantasm>();
}
