using Avalon.Items.Placeable.Bar;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

internal class Oblivionet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Oblivionet");
        ItemID.Sets.CatchingTool[Item.type] = true;
        ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 70;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.rare = ModContent.RarityType<DarkRedRarity>();
        Item.knockBack = 6.2f;
        Item.useTime = 21;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.buyPrice(0, 5);
        Item.useAnimation = 21;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes() => CreateRecipe().AddIngredient(ModContent.ItemType<ExcaliburNet>())
        .AddIngredient(ModContent.ItemType<OblivionBar>(), 10).AddTile(TileID.Anvils).Register();
}
