using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class XanthophyteArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Arrow");
        //Tooltip.SetDefault("Bounces and rains stars down from the heavens");
        SacrificeTotal = 300;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 15;
        Item.shootSpeed = 9f;
        Item.ammo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.XanthophyteArrow>();
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(150)
            .AddIngredient(ItemID.WoodenArrow, 150)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>())
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
