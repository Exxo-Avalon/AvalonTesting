using AvalonTesting.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

internal class SnotlineFishingRod : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Snotline Fishing Rod");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.shootSpeed = 16.5f;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 8;
        Item.fishingPole = 25;
        Item.shoot = ModContent.ProjectileType<SnotlineBobber>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40);
        Item.useAnimation = 8;
    }
}
