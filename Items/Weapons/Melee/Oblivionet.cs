using AvalonTesting.Items.Placeable.Bar;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

internal class Oblivionet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Oblivionet");
        ItemID.Sets.CatchingTool[Item.type] = true;
        ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 70;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.knockBack = 6.2f;
        Item.useTime = 21;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.buyPrice(0, 5);
        Item.useAnimation = 21;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes() => CreateRecipe().AddIngredient(ModContent.ItemType<ExcaliburNet>())
        .AddIngredient(ModContent.ItemType<OblivionBar>(), 10).AddTile(TileID.Anvils).Register();
}
