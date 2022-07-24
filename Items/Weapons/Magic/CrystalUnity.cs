using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Items.Weapons.Magic;

class CrystalUnity : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Crystal Unity");
        Tooltip.SetDefault("'The power of chaos vanquishes your enemies'\n[c/C39FDD:10th Anniversary Contest Winner - FractureACBF]");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 26;
        Item.reuseDelay = 14;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 9f;
        Item.mana = 14;
        Item.rare = ModContent.RarityType<Rarities.FractureRarity>();
        Item.width = dims.Width;
        Item.useTime = 11;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Projectiles.InfectedMist>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 505000;
        Item.useAnimation = 11;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddRecipeGroup("AvalonTesting:GemStaves", 2)
            .AddIngredient(ItemID.CrystalStorm)
            .AddIngredient(ModContent.ItemType<Material.ElementDiamond>(), 2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int x = Main.rand.Next(10);

        if (x == 0) x = ModContent.ProjectileType<Projectiles.CrystalUnity.RubyShard>();
        if (x == 1) x = ModContent.ProjectileType<Projectiles.CrystalUnity.AmberShard>();
        if (x == 2) x = ModContent.ProjectileType<Projectiles.CrystalUnity.TopazShard>();
        if (x == 3) x = ModContent.ProjectileType<Projectiles.CrystalUnity.PeridotShard>();
        if (x == 4) x = ModContent.ProjectileType<Projectiles.CrystalUnity.EmeraldShard>();
        if (x == 5) x = ModContent.ProjectileType<Projectiles.CrystalUnity.TourmalineShard>();
        if (x == 6) x = ModContent.ProjectileType<Projectiles.CrystalUnity.SapphireShard>();
        if (x == 7) x = ModContent.ProjectileType<Projectiles.CrystalUnity.AmethystShard>();
        if (x == 8) x = ModContent.ProjectileType<Projectiles.CrystalUnity.DiamondShard>();
        if (x == 9) x = ModContent.ProjectileType<Projectiles.CrystalUnity.ZirconShard>();

        for (int spread = 0; spread < 5; spread++)
        {
            float xVel = velocity.X;
            float yVel = velocity.Y;
            xVel += Main.rand.Next(-40, 41) * 0.05f;
            yVel += Main.rand.Next(-40, 41) * 0.05f;
            int dmg = Item.damage;
            if (x == ModContent.ProjectileType<Projectiles.CrystalUnity.ZirconShard>()) dmg = (int)(dmg * 2.5f);
            Projectile.NewProjectile(source, position.X, position.Y, xVel, yVel, x, (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
