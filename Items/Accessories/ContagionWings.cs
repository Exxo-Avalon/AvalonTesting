using Avalon.Items.Material;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
internal class ContagionWings : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Contagion Wings");
        Tooltip.SetDefault("Allows flight and slow fall\nOther bonuses apply when in the Contagion");
        SacrificeTotal = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(140, 5f, 1.2f);
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
        if (player.GetModPlayer<ExxoBiomePlayer>().ZoneContagion)
        {
            player.statDefense += 5;
            player.statLifeMax2 += 40;
            if (player.velocity.X != 0 || player.velocity.Y != 0)
            {
                var newColor = default(Color);
                var num = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, ModContent.DustType<Dusts.ContagionDust>(), Main.rand.Next(-3, 3), Main.rand.Next(-3, 3), 100, newColor, 2f);
                Main.dust[num].noGravity = true;
            }
        }

        player.wingTimeMax = 140;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.DemonWings).AddIngredient(ModContent.ItemType<YuckyBit>(), 20)
            .AddIngredient(ModContent.ItemType<Pathogen>(), 25).AddTile(TileID.MythrilAnvil).Register();
    }
}
