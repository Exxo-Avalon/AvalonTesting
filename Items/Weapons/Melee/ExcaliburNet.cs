using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

internal class ExcaliburNet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Exalibur Net");
        ItemID.Sets.CatchingTool[Item.type] = true;
        ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 43;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.knockBack = 4.2f;
        Item.useTime = 23;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.buyPrice(0, 3);
        Item.useAnimation = 23;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<GoldSwordNet>()).AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.Anvils).Register();
        CreateRecipe().AddIngredient(ModContent.ItemType<PlatinumSwordNet>()).AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.Anvils).Register();
        CreateRecipe().AddIngredient(ModContent.ItemType<BismuthSwordNet>()).AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.Anvils).Register();
    }
}
