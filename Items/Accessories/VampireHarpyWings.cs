using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
class VampireHarpyWings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vampire Harpy Wings");
        Tooltip.SetDefault("Allows flight and slow fall and heals life\nOther bonuses apply when in the Dark Matter");
        SacrificeTotal = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(210, 7f, 1.2f);
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.value = 800000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.wingTimeMax = 210;
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneDarkMatter)
        {
            player.statDefense += 8;
            player.lifeRegen += 5;
            if (player.velocity.X != 0 || player.velocity.Y != 0)
            {
                var newColor = default(Color);
                var num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Blood, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, newColor, 2f);
                Main.dust[num].noGravity = true;
            }
        }
    }
}
