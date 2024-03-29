using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class MagicGrenade : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magic Grenade");
        Tooltip.SetDefault("A small explosion that will not destroy tiles");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 85;
        Item.noUseGraphic = true;
        Item.shootSpeed = 8f;
        Item.mana = 40;
        Item.rare = ItemRarityID.Cyan;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 8f;
        Item.useTime = 27;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.MagicGrenade>();
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 27;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.MagicDagger)
            .AddIngredient(ItemID.Grenade, 10)
            .AddIngredient(ItemID.SoulofFright, 20)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
