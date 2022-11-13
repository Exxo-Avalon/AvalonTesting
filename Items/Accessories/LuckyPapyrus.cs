using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class LuckyPapyrus : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lucky Papyrus");
        Tooltip.SetDefault("Provides immunity to Pyroscoric and Tritanorium burns\n7% increased critical strike chance\n25% increased critical strike damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<DarkRedRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.height = dims.Height;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Generic) += 7;
        player.Avalon().CritDamageMult += 0.25f;
        player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
    }
}
