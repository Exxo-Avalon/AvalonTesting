using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
class HolyWings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Holy Wings");
        Tooltip.SetDefault("Allows flight and slow fall\nOther bonuses apply when in the Hallow");
        SacrificeTotal = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(160, 5f, 1.2f);
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 600000;
        Item.accessory = true;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.AngelWings).AddIngredient(ItemID.CrystalShard, 100).AddIngredient(ItemID.PixieDust, 75).AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.wingTimeMax = 160;
        if (player.ZoneHallow)
        {
            player.statLifeMax2 += 60;
            player.statDefense += 4;
            player.buffImmune[142] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Silenced] = true;
            if (player.velocity.X != 0 || player.velocity.Y != 0)
            {
                var newColor = default(Color);
                var num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.HallowedWeapons, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, newColor, 2f);
                Main.dust[num].noGravity = true;
            }
        }
    }
}
