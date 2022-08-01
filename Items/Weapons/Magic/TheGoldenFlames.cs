using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class TheGoldenFlames : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The Golden Flames");
        Tooltip.SetDefault("'The flames are made of gold!'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 72;
        Item.channel = true;
        Item.shootSpeed = 10f;
        Item.crit += 11;
        Item.mana = 14;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.knockBack = 7f;
        Item.useTime = 50;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.GoldenFire>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 250000;
        Item.useAnimation = 50;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 35)
            .AddIngredient(ItemID.SpellTome)
            .AddIngredient(ItemID.SoulofLight, 20)
            .AddIngredient(ItemID.Fireblossom, 5)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
