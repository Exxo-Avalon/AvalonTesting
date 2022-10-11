using System;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class PossessedFlamesaw : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Possessed Flamesaw");
        Tooltip.SetDefault("Right click to shoot a tree-chopping projectile");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 95;
        Item.noUseGraphic = true;
        Item.shootSpeed = 20f;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.width = dims.Width;
        Item.knockBack = 9f;
        Item.useTime = 15;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesaw>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ItemID.AdamantiteChainsaw).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    //    CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ItemID.TitaniumChainsaw).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    //    CreateRecipe(1).AddIngredient(ItemID.PossessedHatchet).AddIngredient(ModContent.ItemType<Tools.TroxiniumChainsaw>()).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddIngredient(ItemID.CursedFlame, 50).AddIngredient(ItemID.LivingFireBlock, 160).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    //}
    public override bool? UseItem(Player player)
    {
        float velX = Main.mouseX + Main.screenPosition.X - player.Center.X;
        float velY = Main.mouseY + Main.screenPosition.Y - player.Center.Y;
        float num72 = (float)Math.Sqrt((double)(velX * velX + velY * velY));
        float num73 = num72;
        num72 = Item.shootSpeed / num72;
        velX *= num72;
        velY *= num72;
        if (player.altFunctionUse == 2)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.MountedCenter, new Vector2(velX, velY), ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesawChop>(), (int)player.GetDamage(DamageClass.Melee).ApplyTo(Item.damage), Item.knockBack, player.whoAmI);
        }
        else
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.MountedCenter, new Vector2(velX, velY), Item.shoot, (int)player.GetDamage(DamageClass.Melee).ApplyTo(Item.damage), Item.knockBack, player.whoAmI);
        }
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        return false;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Melee.PossessedFlamesawChop>()] < 1;
    }
}
