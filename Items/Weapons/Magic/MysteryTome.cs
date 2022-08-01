using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Items.Weapons.Magic;

class MysteryTome : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mystery Tome");
        Tooltip.SetDefault("Casts all spells used to make it in random order\nSpells cast may not match the original");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 110;
        Item.reuseDelay = 14;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 9f;
        Item.mana = 25;
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.useTime = 11;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.InfectedMist>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 505000;
        Item.useAnimation = 11;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<DevilsScythe>()).AddIngredient(ModContent.ItemType<TheGoldenFlames>()).AddIngredient(ModContent.ItemType<Terraspin>()).AddIngredient(ModContent.ItemType<FocusBeam>()).AddIngredient(ModContent.ItemType<Ancient>()).AddIngredient(ModContent.ItemType<TomeoftheDistantPast>()).AddIngredient(ModContent.ItemType<FreezeBolt>()).AddTile(TileID.Bookcases).Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int x = Main.rand.Next(7);

        Vector2 vel = velocity;
        if (x == 0) // Ancient
        {
            SoundEngine.PlaySound(SoundID.Item34, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.AncientSandstorm>(), Item.damage, 4, player.whoAmI);
            return false;
        }
        if (x == 1) // Devil's Scythe
        {
            SoundEngine.PlaySound(SoundID.Item8, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.DevilScythe>(), Item.damage, 5, player.whoAmI);
            return false;
        }
        if (x == 2) // Tome of the Distant Past
        {
            SoundEngine.PlaySound(SoundID.Item8, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.Bones>(), Item.damage, 4, player.whoAmI);
            return false;
        }
        if (x == 3) // The Golden Flames
        {
            SoundEngine.PlaySound(SoundID.Item20, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.GoldenFire>(), Item.damage, 6, player.whoAmI);
            return false;
        }
        if (x == 4) // Focus Beam
        {
            SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Beam"), player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.FocusBeam>(), Item.damage, 5, player.whoAmI);
            return false;
        }
        if (x == 5) // Freeze Bolt
        {
            SoundEngine.PlaySound(SoundID.Item21, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.FreezeBolt>(), Item.damage, 5, player.whoAmI);
            return false;
        }
        if (x == 6) // Terraspin
        {
            SoundEngine.PlaySound(SoundID.Item84, player.position);
            Projectile.NewProjectile(source, position, vel, ModContent.ProjectileType<Projectiles.Magic.TerraTyphoon>(), Item.damage, 5, player.whoAmI);
            return false;
        }

        return true;
    }
}
