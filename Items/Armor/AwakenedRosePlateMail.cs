using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Body)]
internal class AwakenedRosePlateMail : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Awakened Rose Plate Mail");
        Tooltip.SetDefault("Press V to teleport to the cursor"
                           + "\nOn hitting tiles with a magic attack, there is a chance a small vine of thorns shoots out of the ground");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 21;
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 40);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        ExxoPlayer modPlayer = player.Avalon();
        modPlayer.teleportV = true;
        modPlayer.thornMagic = true;
    }
}
