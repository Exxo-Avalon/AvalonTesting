using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class FeroziumPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ferozium Pickaxe");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 34;
        Item.damage = 17;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit += 6;
        Item.pick = 195;
        Item.rare = ItemRarityID.Lime;
        Item.useTime = 15;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 250000;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.FeroziumBar>(), 16)
            .AddIngredient(ModContent.ItemType<Material.FrigidShard>())
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Mod.Find<ModItem>("FeroziumPickaxe").Type)
        {
            player.pickSpeed -= 0.5f;
        }
    }
}
