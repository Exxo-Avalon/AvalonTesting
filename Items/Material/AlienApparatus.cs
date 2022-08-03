using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class AlienApparatus : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Alien Apparatus");
        Tooltip.SetDefault("Used for crafting the Eye of Oblivion\n'It says \"hello.\" I think...'");
        SacrificeTotal = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.maxStack = 999;
        Item.value = 0;
        Item.height = dims.Height;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Vwomp");
        Item.useStyle = ItemUseStyleID.Thrust;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<DragonScale>(), 3).AddIngredient(ModContent.ItemType<SoulofDelight>(), 5).AddIngredient(ModContent.ItemType<DarkMatterGel>(), 20).AddIngredient(ModContent.ItemType<AlienDevice>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
