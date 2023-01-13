using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class StaminaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stamina Potion");
        Tooltip.SetDefault("Restores 55 stamina");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1] { Color.Green };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 55;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 50;
        Item.value = 900;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<LesserStaminaPotion>()).AddIngredient(ItemID.GlowingMushroom).AddTile(TileID.Bottles).Register();
    }
    public override bool CanUseItem(Player player)
    {
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam >= player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2) return false;
        return true;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().StatStam += 55;
        player.GetModPlayer<ExxoStaminaPlayer>().StaminaHealEffect(55, true);
        player.AddBuff(ModContent.BuffType<Buffs.StaminaDrain>(), 60 * 9);
        if (player.GetModPlayer<ExxoStaminaPlayer>().StatStam > player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2)
        {
            player.GetModPlayer<ExxoStaminaPlayer>().StatStam = player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2;
        }
        return true;
    }
}
