using Avalon.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

internal class SnotlineFishingRod : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Snotline Fishing Rod");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 28;
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
