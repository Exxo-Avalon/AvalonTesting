using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class SpikeRailgun : ModItem
{
    private int fireDelay = 90;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spike Railgun");
        Tooltip.SetDefault("Uses spikes for ammo\nShoots spikes at extreme velocity");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 24;
        Item.damage = 120;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = ItemID.Spike;
        Item.shootSpeed = 30f;
        Item.crit += 2;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
        Item.noMelee = true;
        //Item.channel = true;
        Item.knockBack = 8f;
        Item.useTime = 30;
        Item.shoot = 1;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 30;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Charge");
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        return false;
    }
    public override void HoldItem(Player player)
    {
        if (fireDelay > 0 && player.itemAnimation > 0) fireDelay--;
        if (fireDelay == 89)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), player.position, player.velocity, ModContent.ProjectileType<Projectiles.Ranged.SpikeRailgunCharge>(), 0, 0f, player.whoAmI);
        }
        else if (fireDelay == 0)
        {
            float velX = Main.mouseX + Main.screenPosition.X - player.Center.X;
            float velY = Main.mouseY + Main.screenPosition.Y - player.Center.Y;
            float num72 = (float)Math.Sqrt((double)(velX * velX + velY * velY));
            float num73 = num72;
            num72 = Item.shootSpeed / num72;
            velX *= num72;
            velY *= num72;
            //if (player.gravDir == -1f)
            //{
            //    velY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - player.Center.Y;
            //}
            //float v = MathHelper.Clamp(velX, -7f, 7f);
            //if (v < 0 && v > -5f) v = -5f;
            //if (v > 0 && v < 5f) v = 5f;

            #region find ammo
            var item2 = new Item();
            bool flag7 = false;
            bool inAmmoSlots = false;
            for (int i = 54; i < 58; i++)
            {
                if (player.inventory[i].ammo == player.inventory[player.selectedItem].useAmmo &&
                    player.inventory[i].stack > 0)
                {
                    item2 = player.inventory[i];
                    flag7 = true;
                    inAmmoSlots = true;
                    break;
                }
            }

            if (!inAmmoSlots)
            {
                for (int i = 0; i < 54; i++)
                {
                    if (player.inventory[i].ammo == player.inventory[player.selectedItem].useAmmo &&
                        player.inventory[i].stack > 0)
                    {
                        item2 = player.inventory[i];
                        flag7 = true;
                        break;
                    }
                }
            }
            #endregion find ammo

            if (flag7)
            {
                if (player.inventory[player.selectedItem].useAmmo == ItemID.Spike)
                {
                    int t = 0;
                    int dmgAdd = 0;
                    if (item2.type == ItemID.Spike)
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.SpikeCannon>();
                        dmgAdd = 11;
                    }
                    else if (item2.type == ModContent.ItemType<Placeable.Tile.DemonSpikeScale>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.DemonSpikeScale>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Placeable.Tile.BloodiedSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.BloodiedSpike>();
                        dmgAdd = 17;
                    }
                    else if (item2.type == ModContent.ItemType<Placeable.Tile.NastySpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.NastySpike>();
                        dmgAdd = 18;
                    }
                    else if (item2.type == ItemID.WoodenSpike)
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.WoodenSpike>();
                        dmgAdd = 30;
                    }
                    else if (item2.type == ModContent.ItemType<Placeable.Tile.VenomSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.VenomSpike>();
                        dmgAdd = 39;
                    }
                    else if (item2.type == ModContent.ItemType<Placeable.Tile.PoisonSpike>())
                    {
                        t = ModContent.ProjectileType<Projectiles.Ranged.PoisonSpike>();
                        dmgAdd = 15;
                    }

                    if (t > 0)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_ItemUse_WithPotentialAmmo(Item, Item.ammo), player.position,
                            new Vector2(velX, velY), t, Item.damage + dmgAdd, Item.knockBack, player.whoAmI);
                    }
                }
            }

            //int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.position.X,
            //    player.position.Y, velX, velY, ModContent.ProjectileType<Projectiles.Melee.Shell>(), 87, 6f);
            //Main.projectile[p].owner = player.whoAmI;
            fireDelay = 90;
        }
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10, 0);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<SpikeCannon>())
            .AddIngredient(ModContent.ItemType<Material.Phantoplasm>(), 25)
            .AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 15)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
