using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

class OnyxHook : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Onyx Hook");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.damage = 70;
        Item.useTurn = true;
        Item.shootSpeed = 16f;
        Item.rare = ItemRarityID.Lime;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 25f;
        Item.shoot = ModContent.ProjectileType<Projectiles.OnyxHook>();
        Item.value = Item.sellPrice(0, 9, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Onyx>(), 20)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 5)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
