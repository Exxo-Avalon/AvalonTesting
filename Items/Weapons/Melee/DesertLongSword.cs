using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class DesertLongSword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Desert Longsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 29;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 27;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 54, 0);
        Item.useAnimation = 27;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.SandBlock, 60)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Beak>(), 5)
            .AddIngredient(ItemID.Topaz, 5)
            .AddTile(TileID.Anvils).Register();
    }
}
