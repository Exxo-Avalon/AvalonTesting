using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class SolariumStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Solarium Staff");
        Tooltip.SetDefault("Fires a fiery ball that bursts into fiery sparks\n'Oxygen Devourer'");
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
        Item.damage = 145;
        Item.autoReuse = true;
        Item.shootSpeed = 9f;
        Item.mana = 19;
        Item.rare = ItemRarityID.Cyan;
        Item.knockBack = 6f;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.shoot = ModContent.ProjectileType<Projectiles.SolarBolt>();
        Item.value = Item.sellPrice(0, 10, 0, 0);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.SolariumStar>(), 40)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.FeroziumBar>(), 7)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
