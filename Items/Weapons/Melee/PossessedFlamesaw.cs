using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class PossessedFlamesaw : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Possessed Flamesaw"); // make chop ability a right click
        Tooltip.SetDefault("Can chop trees instantly");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 95;
        Item.noUseGraphic = true;
        Item.shootSpeed = 14f;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.width = dims.Width;
        Item.knockBack = 9f;
        Item.useTime = 15;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesaw>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ItemID.AdamantiteChainsaw).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ItemID.TitaniumChainsaw).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ModContent.ItemType<Tools.TroxiniumChainsaw>()).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
