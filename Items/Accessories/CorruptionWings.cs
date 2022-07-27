using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
class CorruptionWings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Corruption Wings");
        Tooltip.SetDefault("Allows flight and slow fall\nOther bonuses apply when in the Corruption");
        SacrificeTotal = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(140, 5f, 1.2f);
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 400000;
        Item.accessory = true;
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.ZoneCorrupt)
        {
            player.statDefense += 5;
            player.statLifeMax2 += 40;
            if (player.velocity.X != 0 || player.velocity.Y != 0)
            {
                var newColor = default(Color);
                var num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.CorruptPlants, Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, newColor, 2f);
                Main.dust[num].noGravity = true;
            }
        }
        player.wingTime = 140;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.DemonWings).AddIngredient(ItemID.RottenChunk, 20).AddIngredient(ItemID.CursedFlame, 25).AddTile(TileID.MythrilAnvil).Register();
    }
}
