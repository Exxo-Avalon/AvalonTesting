using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class SonicScrewdriverMkII : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sonic Screwdriver Mk II");
        Tooltip.SetDefault("Reveals treasures, ores, and mobs\nTells time and shows position");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.height = dims.Height;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<SonicScrewdriverMkI>())
            .AddIngredient(ItemID.Sapphire, 7)
            .AddIngredient(ItemID.Wire, 10)
            .AddIngredient(ItemID.GPS)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
