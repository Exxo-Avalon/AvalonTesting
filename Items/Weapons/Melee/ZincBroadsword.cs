using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class ZincBroadsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Zinc Broadsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 13;
        Item.useTurn = true;
        Item.scale = 1.12f;
        Item.width = dims.Width;
        Item.useTime = 23;
        Item.knockBack = 5.2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 5500;
        Item.useAnimation = 23;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.ZincBar>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
