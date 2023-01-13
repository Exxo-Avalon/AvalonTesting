using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.AdvancedPotions;

class AdvSonarPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Sonar Elixir");
        Tooltip.SetDefault("Detects hooked fish");
        SacrificeTotal = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            new Color(217, 254, 161),
            new Color(69, 110, 9),
            new Color(135, 219, 11)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvSonar>();
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
        Item.buffTime = 19600 * 2;
    }
}
