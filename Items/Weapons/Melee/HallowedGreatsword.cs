using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class HallowedGreatsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hallowed Greatsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 46;
        Item.height = 50;
        Item.damage = 72;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.5f;
        Item.rare = ItemRarityID.Pink;
        Item.useTime = 26;
        Item.knockBack = 2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 55, 0);
        Item.useAnimation = 26;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 35)
            .AddIngredient(ItemID.SoulofMight, 20)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
