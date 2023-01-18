using Avalon.Items.Material;
using Avalon.Items.Placeable.Bar;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class SoulEdge : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul Edge");
        Tooltip.SetDefault("'Haunted by souls of darkness'"); // use paper airplane projectile code to maybe fix?
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.UseSound = SoundID.Item1;
        Item.damage = 92;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.shootSpeed = 6f;
        Item.rare = ModContent.RarityType<Rarities.DarkRedRarity>();
        Item.noMelee = true;
        Item.useTime = 20;
        Item.knockBack = 6.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.SoulEdgeSlash>(); //ProjectileID.LostSoulFriendly;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 80, 0, 0);
        Item.useAnimation = 20;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 150);
    }
    /*public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.TerraBlade)
            .AddIngredient(ItemID.SpectreStaff)
            .AddIngredient(ModContent.ItemType<SoulofBlight>(), 40)
            .AddIngredient(ModContent.ItemType<SuperhardmodeBar>(), 5)
            .AddIngredient(ItemID.Ectoplasm, 60)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }*/
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, new Vector2(0.1f,0).RotatedBy(velocity.ToRotation()), ModContent.ProjectileType<Projectiles.Melee.SoulEdgeSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax * 1.3f);

        int p1 = Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<Projectiles.Melee.SoulEdgeSlash2>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);

        int numberProjectiles = 2 + Main.rand.Next(2); // 4 or 5 shots
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(25));
            int spirit = Projectile.NewProjectile(source, position + new Vector2(30 * Item.scale, 0).RotatedBy(velocity.ToRotation()), perturbedSpeed, ModContent.ProjectileType<Projectiles.Melee.Soul>(), damage, knockback, player.whoAmI);
            Main.projectile[spirit].DamageType = DamageClass.Melee;
            Main.projectile[spirit].timeLeft = Main.rand.Next(500, 600);
        }
        return false; // return false because we don't want tmodloader to shoot projectile
    }
}
