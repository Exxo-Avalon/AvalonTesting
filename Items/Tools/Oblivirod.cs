using AvalonTesting.Projectiles;
using AvalonTesting.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

internal class Oblivirod : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Oblivirod");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.shootSpeed = 15.5f;
        Item.rare = ModContent.RarityType<RainbowRarity>();
        Item.useTime = 8;
        Item.fishingPole = 110;
        Item.shoot = ModContent.ProjectileType<OblivirodBobber>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40);
        Item.useAnimation = 8;
    }
}
