using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class Infernasword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Infernasword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 38;
        Item.damage = 80;
        Item.autoReuse = true;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.Lime;
        Item.knockBack = 4f;
        Item.useTime = 20;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.InfernoScythe>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5);
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = false;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.EnchantedSword).AddIngredient(ItemID.LivingFireBlock, 100).AddIngredient(ItemID.SoulofMight, 16).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 7).AddTile(TileID.MythrilAnvil).Register();
    }
}
