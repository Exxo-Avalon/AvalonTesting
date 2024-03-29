using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class DarklightLance : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Darklight Lance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 99;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 4f;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.knockBack = 5.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.DarklightLance>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 40, 0, 0);
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.DarkLance)
            .AddIngredient(ItemID.Gungnir)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 25)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.LightShard)
            .AddTile(TileID.AdamantiteForge).Register();
    }
}
