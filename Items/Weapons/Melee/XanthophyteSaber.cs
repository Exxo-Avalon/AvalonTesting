using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class XanthophyteSaber : ModItem
{
    // TODO: ADD PROJECTILE
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Xanthophyte Saber");
        //Tooltip.SetDefault("'The unification of the Elements'\n'A relic of the past'");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.UseSound = SoundID.Item1;
        Item.damage = 60;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 15;
        Item.knockBack = 4.2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 52, 0);
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
