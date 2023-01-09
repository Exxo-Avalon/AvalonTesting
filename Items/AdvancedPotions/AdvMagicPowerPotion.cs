using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvMagicPowerPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magic Power Elixir");
        Tooltip.SetDefault("40% increased magic damage");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(66, 13, 137),
            new Color(103, 10, 230),
            new Color(163, 95, 255)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvMagicPower>();
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
        Item.buffTime = 36000;
    }
}
