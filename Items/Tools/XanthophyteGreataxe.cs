using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class XanthophyteGreataxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Greataxe");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 72;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.axe = 23;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost++;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 4, 32);
        Item.useAnimation = 30;
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
}
