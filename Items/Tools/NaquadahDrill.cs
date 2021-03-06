using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class NaquadahDrill : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Naquadah Drill");
        Tooltip.SetDefault("Can mine Adamantite, Titanium, and Troxinium");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 12;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.shootSpeed = 32f;
        Item.pick = 150;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 13;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.NaquadahDrill>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 102500;
        Item.useAnimation = 25;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item23;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.NaquadahBar>(), 15)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
