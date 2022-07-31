using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class SpiritbeamFork : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spiritbeam Fork");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 60;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.mana = 15;
        Item.rare = ItemRarityID.Cyan;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.knockBack = 4.25f;
        Item.shoot = ModContent.ProjectileType<Projectiles.ShadowSpirit>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 30, 0, 0);
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item43;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.InfernoFork).AddIngredient(ItemID.SpectreStaff).AddIngredient(ItemID.ShadowbeamStaff).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
