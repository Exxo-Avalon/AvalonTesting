using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvInfernoPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Inferno Elixir");
        Tooltip.SetDefault("Ignites nearby enemies");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(255, 227, 0),
            new Color(255, 135, 0),
            new Color(226, 56, 0)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvInferno>();
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
        Item.buffTime = 28800;
    }
}
