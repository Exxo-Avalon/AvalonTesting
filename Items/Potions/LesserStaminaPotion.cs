using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class LesserStaminaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lesser Stamina Potion");
        Tooltip.SetDefault("Restores 35 stamina");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1] { Color.Green };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 35;
        Item.maxStack = 30;
        Item.value = 400;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Boltstone>()).AddIngredient(ItemID.Cactus, 2).AddTile(TileID.Bottles).Register();
    }
    public override bool CanUseItem(Player player)
    {
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2) return false;
        return true;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().StatStam += 35;
        player.GetModPlayer<ExxoStaminaPlayer>().StaminaHealEffect(35, true);
        player.AddBuff(ModContent.BuffType<Buffs.StaminaDrain>(), 60 * 9);
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam > player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2)
        {
            player.GetModPlayer<ExxoStaminaPlayer>().StatStam = player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2;
        }
        return true;
    }
}
