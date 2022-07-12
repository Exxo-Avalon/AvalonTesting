using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Consumables;

class StaminaCrystal : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Stamina Crystal");
        Tooltip.SetDefault("Permanently increases maximum stamina by 30");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.maxStack = 999;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item29;
        Item.value = 95000;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        return player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax < 300;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax += 30;
        player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 += 30;
        player.GetModPlayer<ExxoStaminaPlayer>().StatStam += 30;
        return true;
    }
}
