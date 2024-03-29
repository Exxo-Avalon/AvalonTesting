using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class SonicScrewdriverMkIII : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sonic Screwdriver Mk III");
        Tooltip.SetDefault("Reveals treasures, ores, mobs, and dangers\nTells time, shows position, and can open all locks");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.Cyan;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<SonicScrewdriverMkII>())
            .AddIngredient(ItemID.Emerald, 10)
            .AddIngredient(ItemID.Wire, 20)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddIngredient(ModContent.ItemType<Material.Onyx>(), 10)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
