using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvMagnetPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magnet Elixir");
        Tooltip.SetDefault("Increases item grab range");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] { Color.Red, Color.Blue };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvMagnet>();
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
        Item.buffTime = 8 * 60 * 60;
    }
}
