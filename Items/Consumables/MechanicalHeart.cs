using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class MechanicalHeart : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mechanical Heart");
        Tooltip.SetDefault("Permanently increases accessory slots by 1");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item4;
        Item.consumable = true;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.maxStack = 999;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool CanUseItem(Player player)
    {
        return !player.GetModPlayer<ExxoPlayer>().shmAcc;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<ExxoPlayer>().shmAcc = true;
        return true;
    }
}
