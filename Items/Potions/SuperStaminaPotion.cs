using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Potions;

class SuperStaminaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Super Stamina Potion");
        Tooltip.SetDefault("Restores 120 stamina");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.EatFood;
        Item.GetGlobalItem<AvalonTestingGlobalItemInstance>().healStamina = 120;
        Item.maxStack = 99;
        Item.value = 4000;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(2).AddIngredient(ModContent.ItemType<GreaterStaminaPotion>(), 2).AddIngredient(ItemID.ChlorophyteBar).AddIngredient(ItemID.SharkFin, 2).AddTile(TileID.Bottles).Register();
        CreateRecipe(2).AddIngredient(ModContent.ItemType<GreaterStaminaPotion>(), 2).AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>()).AddIngredient(ItemID.SharkFin, 2).AddTile(TileID.Bottles).Register();
    }
    public override bool CanUseItem(Player player)
    {
        if (player.Avalon().StatStam >= player.Avalon().StatStamMax2) return false;
        return true;
    }
    public override bool? UseItem(Player player)
    {
        player.Avalon().StatStam += 120;
        player.Avalon().StaminaHealEffect(120, true);
        if (player.Avalon().StatStam > player.Avalon().StatStamMax2)
        {
            player.Avalon().StatStam = player.Avalon().StatStamMax2;
        }
        return true;
    }
}
