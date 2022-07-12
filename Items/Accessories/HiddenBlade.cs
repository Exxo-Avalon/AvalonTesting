using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

class HiddenBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hidden Blade");
        Tooltip.SetDefault("Immunity to Broken Weaponry");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = 100000;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[ModContent.BuffType<Buffs.BrokenWeaponry>()] = true;
    }
}
