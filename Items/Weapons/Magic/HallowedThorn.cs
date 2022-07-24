using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class HallowedThorn : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hallowed Thorn");
        Tooltip.SetDefault("Summons a splitting, hallow thorn");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 28;
        Item.shootSpeed = 32f;
        Item.mana = 20;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 28;
        Item.knockBack = 2f;
        Item.shoot = ModContent.ProjectileType<Projectiles.HallowedThorn>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 20000;
        Item.useAnimation = 28;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 8)
            .AddIngredient(ItemID.SoulofFright, 15)
            .AddIngredient(ItemID.LightShard, 3)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
