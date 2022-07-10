using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class Bayonet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bayonet");
        Tooltip.SetDefault("Immunity to Broken Weaponry and Unloaded");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<HiddenBlade>())
            .AddIngredient(ModContent.ItemType<AmmoMagazine>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[ModContent.BuffType<Buffs.BrokenWeaponry>()] = true;
        player.buffImmune[ModContent.BuffType<Buffs.Unloaded>()] = true;
    }
}
