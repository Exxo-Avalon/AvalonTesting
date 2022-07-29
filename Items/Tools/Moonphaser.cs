using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class Moonphaser : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Moonphaser");
        Tooltip.SetDefault("Changes the phases of the Moon\nHas a chance to trigger a Blood Moon if night");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.shoot = ModContent.ProjectileType<Projectiles.Moonphaser>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 70, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Lens, 5)
            .AddIngredient(ItemID.SoulofLight, 10)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddRecipeGroup("Avalon:GoldBar", 20)
            .AddIngredient(ItemID.BlackLens)
            .AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 4)
            .AddTile(TileID.WorkBenches).Register();
    }
}
