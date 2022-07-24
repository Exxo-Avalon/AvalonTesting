using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class GreaterStaminaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Greater Stamina Potion");
        Tooltip.SetDefault("Restores 95 stamina");
        SacrificeTotal = 30;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 95;
        Item.maxStack = 75;
        Item.value = 2000;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(10).AddIngredient(ModContent.ItemType<StaminaPotion>(), 10).AddIngredient(ItemID.Feather, 2).AddIngredient(ItemID.SoulofFlight).AddTile(TileID.Bottles).Register();
    }
    public override bool CanUseItem(Player player)
    {
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2) return false;
        return true;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().StatStam += 95;
        player.GetModPlayer<ExxoStaminaPlayer>().StaminaHealEffect(95, true);
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam > player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2)
        {
            player.GetModPlayer<ExxoStaminaPlayer>().StatStam = player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2;
        }
        return true;
    }
}
