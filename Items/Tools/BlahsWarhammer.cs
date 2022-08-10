using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class BlahsWarhammer : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah's Warhammer");
        Tooltip.SetDefault("You shouldn't have this");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 48;
        Item.UseSound = SoundID.Item1;
        Item.damage = 80;
        Item.autoReuse = true;
        Item.hammer = 250;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.useTime = 9;
        Item.knockBack = 20f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 6;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 1016000;
        Item.useAnimation = 9;
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Item.type)
        {
            player.wallSpeed += 0.5f;
        }
    }
}
