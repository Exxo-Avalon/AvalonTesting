using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class BlahPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah Potion");
        Tooltip.SetDefault("Various effects");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Blah>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = false;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 1;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 5 * 60 * 60 * 60; // 5 hours
    }
}
