using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class BronzeBroadsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Broadsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinBroadsword);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BronzeBar>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
