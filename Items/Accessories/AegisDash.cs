using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Shield)]
public class AegisDash : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aegis Dash");
        Tooltip.SetDefault("Dash into enemies to damage them");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        ExxoDashPlayer.RegisterNewDashItem(Type,
            new ExxoDashPlayer.DashInfo(new[] {ExxoDashPlayer.DashDirection.Left, ExxoDashPlayer.DashDirection.Right},
                50, 35, 15));
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 70;
        Item.rare = ItemRarityID.Yellow;
        Item.knockBack = 10f;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 7);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoDashPlayer>().QueueDashEffect(this);
    }
}
