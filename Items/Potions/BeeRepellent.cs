using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class BeeRepellent : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bee Repellent");
        Tooltip.SetDefault("Provides immunity to Hornets");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.Goldenrod
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.BeeSweet>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 21600;
    }
}
