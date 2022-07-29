using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class ShadowRing : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shadow Ring");
        Tooltip.SetDefault("Negates visual cloaking from stealth armors\nWorks in the vanity slot");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.ShroomiteBar, 5)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Onyx>(), 2)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateVanity(Player player)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().ShadowRing = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().ShadowRing = true;
    }
}
