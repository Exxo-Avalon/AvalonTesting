using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AvalonTesting.Items.Weapons.Melee;

class CaesiumMace : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Mace");
        Tooltip.SetDefault("Explodes with each hit");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 71;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.scale = 1.1f;
        Item.shootSpeed = 25f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 40;
        Item.knockBack = 9f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.CaesiumFlail>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 54000;
        Item.useAnimation = 40;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CaesiumBar>(), 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
