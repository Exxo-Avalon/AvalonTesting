using AvalonTesting.Buffs;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Accessories;

public class AIController : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("AI Controller");
        Tooltip.SetDefault("Stinger probe minions circle you"
                           + "\nThis probe will reflect hostile projectiles and explode"
                           + "\nProbes will regenerate over time, to a max of four");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = -12;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15);
        Item.buffType = ModContent.BuffType<StingerProbe>();
        Item.height = dims.Height;
        Item.expert = true;
        Item.damage = 134;
        Item.DamageType = DamageClass.Summon;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!player.HasBuff(Item.buffType))
        {
            player.GetModPlayer<ExxoBuffPlayer>().StingerProbeTimer = 0;
        }

        player.AddBuff(Item.buffType, 2);
    }
}
