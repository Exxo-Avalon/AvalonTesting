using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions;

class ForceFieldPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Force Field Potion");
        Tooltip.SetDefault("Enables a projectile-reflecting force field");
        SacrificeTotal = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
            Color.Orange,
            Color.Goldenrod
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.ForceField>();
        Item.consumable = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 18000;
        Item.UseSound = SoundID.Item3;
    }
}
