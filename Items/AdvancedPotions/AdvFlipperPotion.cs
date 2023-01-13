using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvFlipperPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Flipper Elixir");
        Tooltip.SetDefault("Lets you move swiftly in liquids");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(95, 194, 255),
            new Color(12, 109, 167),
            new Color(13, 76, 115)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvFlipper>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 4, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 28800 * 2;
    }
}
