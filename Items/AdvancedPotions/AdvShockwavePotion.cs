using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvShockwavePotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Shockwave Elixir");
        Tooltip.SetDefault("Enemies take damage when you land");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] { Color.Brown, Color.Green };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvShockwave>();
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
        Item.buffTime = 50400;
    }
}
