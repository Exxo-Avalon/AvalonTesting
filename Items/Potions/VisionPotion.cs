using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class VisionPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vision Potion");
        Tooltip.SetDefault("Open caves light up\nCurrently not functional");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.Green
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Vision>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.value = 2000;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 30;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 5400;
        Item.UseSound = SoundID.Item3;
    }
}
