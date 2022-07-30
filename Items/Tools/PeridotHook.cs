using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class PeridotHook : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Peridot Hook");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 16f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.PeridotHook>();
        Item.value = Item.sellPrice(0, 0, 54, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Peridot>(), 15)
            .AddTile(TileID.Anvils).Register();
    }
}
