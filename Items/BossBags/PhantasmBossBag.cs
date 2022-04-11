using AvalonTesting.Items.Accessories;
using AvalonTesting.Items.Material;
using AvalonTesting.Items.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.BossBags;

public class PhantasmBossBag : ModItem
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
        Item.width = 24;
        Item.height = 24;
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
        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<Items.Accessories.AstrallineArtifact>());
        int drop = Main.rand.Next(3);
        if (drop == 0)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<VampireTeeth>());
        }
        else if (drop == 1)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<PhantomKnives>());
        }
        else if (drop == 2)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<EtherealHeart>());
        }
        player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<GhostintheMachine>(), Main.rand.Next(3, 6));
    }

    public override int BossBagNPC => ModContent.NPCType<NPCs.Bosses.Phantasm>();
}
