using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class XanthophyteClub : ModItem
{
    // TODO: ADD PROJECTILE
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Club");
        //Tooltip.SetDefault("'The unification of the Elements'\n'A relic of the past'");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 97;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 24;
        Item.knockBack = 6.1f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 52, 0);
        Item.useAnimation = 24;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
