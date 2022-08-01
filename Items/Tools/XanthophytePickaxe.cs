using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class XanthophytePickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Pickaxe");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 40;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.pick = 202;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 24;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost++;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 4, 32);
        Item.useAnimation = 24;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 18)
            .AddIngredient(ModContent.ItemType<Material.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Type)
        {
            player.pickSpeed -= 0.05f;
        }
    }
}
