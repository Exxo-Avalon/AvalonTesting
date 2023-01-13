using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvStrengthPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Strength Elixir");
        Tooltip.SetDefault("Increases all stats");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] { Color.DarkBlue, Color.Indigo };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvStrength>();
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
        Item.buffTime = 3600 * 18;
    }
}
