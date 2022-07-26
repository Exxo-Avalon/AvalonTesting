using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class BronzeBow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bronze Bow");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.TinBow);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BronzeBar>(), 7)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
