using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class NickelPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nickel Pickaxe");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 6;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.pick = 44;
        Item.width = dims.Width;
        Item.useTime = 12;
        Item.knockBack = 2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 2500;
        Item.useAnimation = 19;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.NickelBar>(), 12)
            .AddRecipeGroup(RecipeGroupID.Wood, 4)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
