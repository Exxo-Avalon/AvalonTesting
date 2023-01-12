using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class ElementalExcalibur : ModItem
{
    int swingCounter = 0;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elemental Excalibur");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 46;
        Item.damage = 190;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.scale = 1.2f;
        Item.shootSpeed = 13f;
        Item.rare = ModContent.RarityType<Rarities.RainbowRarity>();
        Item.noMelee = false;
        Item.useTime = 15;
        Item.knockBack = 8.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ElementWaterBeam>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 90, 0, 0);
        Item.useAnimation = 10;
        Item.useTurn = false;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 255);
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        int randomNum = Main.rand.Next(8);
        if (randomNum == 0) target.AddBuff(BuffID.Poisoned, 300);
        else if (randomNum == 1) target.AddBuff(BuffID.OnFire, 200);
        else if (randomNum == 2) target.AddBuff(BuffID.Confused, 120);
        else if (randomNum == 3) target.AddBuff(BuffID.CursedInferno, 300);
        else if (randomNum == 4) target.AddBuff(BuffID.Frostburn, 300);
        else if (randomNum == 5) target.AddBuff(BuffID.Venom, 240);
        else if (randomNum == 6) target.AddBuff(BuffID.Ichor, 300);
        else if (randomNum == 7)
        {
            if (Logic.CanBeFrozen.CanFreeze(target))
                target.AddBuff(ModContent.BuffType<Buffs.Frozen>(), 60);
            else
                randomNum = Main.rand.Next(7);
        }
    }
    public override void OnHitPvp(Player player, Player target, int damage, bool crit)
    {
        int randomNum = Main.rand.Next(8);
        if (randomNum == 0) target.AddBuff(BuffID.Poisoned, 300);
        else if (randomNum == 1) target.AddBuff(BuffID.OnFire, 200);
        else if (randomNum == 2) target.AddBuff(BuffID.Confused, 120);
        else if (randomNum == 3) target.AddBuff(BuffID.CursedInferno, 300);
        else if (randomNum == 4) target.AddBuff(BuffID.Frostburn, 300);
        else if (randomNum == 5) target.AddBuff(BuffID.Venom, 240);
        else if (randomNum == 6) target.AddBuff(BuffID.Ichor, 300);
        else if (randomNum == 7) target.AddBuff(BuffID.Frozen, 60);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        swingCounter++;
        if (swingCounter == 1)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<Projectiles.Melee.ElementEarthBeam>(), damage, knockback, player.whoAmI);
        }
        else if (swingCounter == 2)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<Projectiles.Melee.ElementFireBeam>(), damage, knockback, player.whoAmI);
        }
        else if (swingCounter == 3)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<Projectiles.Melee.ElementMetalBeam>(), damage, knockback, player.whoAmI);
        }
        else if (swingCounter == 4)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<Projectiles.Melee.ElementWaterBeam>(), damage, knockback, player.whoAmI);
        }
        else if (swingCounter == 5)
        {
            Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, velocity, ModContent.ProjectileType<Projectiles.Melee.ElementWoodBeam>(), damage, knockback, player.whoAmI);
        }
        else swingCounter = 0;
        //for (int i = 0; i < 5; i++)
        //{
        //    float vX = velocity.X + Main.rand.Next(-80, 81) * 0.05f;
        //    float vY = velocity.Y + Main.rand.Next(-80, 81) * 0.05f;
        //    Vector2 v = new Vector2(vX, vY);
        //    if (i == 0)
        //    {
        //        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, v, ModContent.ProjectileType<Projectiles.Melee.ElementEarthBeam>(), damage, knockback);
        //    }
        //    if (i == 1)
        //    {
        //        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, v, ModContent.ProjectileType<Projectiles.Melee.ElementFireBeam>(), damage, knockback);
        //    }
        //    if (i == 2)
        //    {
        //        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, v, ModContent.ProjectileType<Projectiles.Melee.ElementMetalBeam>(), damage, knockback);
        //    }
        //    if (i == 3)
        //    {
        //        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, v, ModContent.ProjectileType<Projectiles.Melee.ElementWaterBeam>(), damage, knockback);
        //    }
        //    if (i == 4)
        //    {
        //        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), position, v, ModContent.ProjectileType<Projectiles.Melee.ElementWoodBeam>(), damage, knockback);
        //    }
        //}
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.TerraBlade)
            .AddIngredient(ModContent.ItemType<VertexofExcalibur>())
            .AddIngredient(ModContent.ItemType<TrueAeonsEternity>())
            .AddIngredient(ModContent.ItemType<Material.SoulofDelight>(), 20)
            .AddIngredient(ModContent.ItemType<Material.ElementShard>(), 10)
            .AddTile(ModContent.TileType<Tiles.SolariumAnvil>())
            .Register();
    }
}
