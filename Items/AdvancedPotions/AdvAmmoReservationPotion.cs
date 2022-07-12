using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.AdvancedPotions;

class AdvAmmoReservationPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ammo Reservation Elixir");
        Tooltip.SetDefault("Gives 30% chance to not consume ammo");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvAmmoReservation>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;

        Item.buffTime = 50400;
    }
}
