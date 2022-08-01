using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class PeridotStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Peridot Staff");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SapphireStaff);
        Item.staff[Item.type] = true;
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.damage = 21;
        Item.autoReuse = true;
        Item.shootSpeed = 7.75f;
        Item.mana = 7;
        Item.rare = ItemRarityID.Blue;
        Item.useTime = 31;
        Item.useAnimation = 31;
        Item.knockBack = 4.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.PeridotBolt>();
        Item.value = Item.buyPrice(0, 3, 60, 0);
        Item.UseSound = SoundID.Item43;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Peridot>(), 15)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.ZincBar>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
