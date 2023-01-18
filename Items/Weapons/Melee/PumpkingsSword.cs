using System;
using System.Linq.Expressions;
using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class PumpkingsSword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The Pumpking Carver");
        SacrificeTotal = 1;
    }
    public SoundStyle PumpkingSwordSwing = new SoundStyle("Terraria/Sounds/Custom/dd2_sonic_boom_blade_slash_1", SoundType.Sound)
    {
        Volume = 0.5f,
        MaxInstances = 10,
        PitchVariance = 0.2f
    };
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 46;
        Item.damage = 300;
        Item.autoReuse = true;
        Item.UseSound = PumpkingSwordSwing;
        Item.scale = 1f;
        Item.rare = ModContent.RarityType<BlueRarity>();
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.knockBack = 8f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.PumpkingsBeam>();
        Item.shootSpeed = 12f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 40, 0, 0);
        Item.noMelee = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.TheHorsemansBlade).AddIngredient(ItemID.SpookyWood, 900).AddIngredient(ItemID.LivingFireBlock, 200).AddIngredient(ItemID.Pumpkin, 30).AddIngredient(ModContent.ItemType<Material.SoulofBlight>(), 20).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 255);
    }
    /*
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        pumpkinSword(target.whoAmI, (int)(damage * 2), knockBack, player);
    }
    */

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        //Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<Projectiles.Melee.PumpkingSwordSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        //int p = Projectile.NewProjectile(source, position, new Vector2(player.direction, 0).RotatedBy(MathHelper.Pi * -0.75f * player.direction), ModContent.ProjectileType<Projectiles.Melee.PumpkingSwordSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        //Main.projectile[p].ai[0] *= 1.5f;
        return false;
    }
    /*
    private void pumpkinSword(int i, int dmg, float kb, Player p)
    {
        //if (Main.rand.Next(2) == 1)
        {
            int logicCheckScreenHeight = Main.LogicCheckScreenHeight;
            int logicCheckScreenWidth = Main.LogicCheckScreenWidth;
            int num = Main.rand.Next(100, 300);
            int num2 = Main.rand.Next(100, 300);
            num = ((Main.rand.Next(2) != 0) ? (num + (logicCheckScreenWidth / 2 - num)) : (num - (logicCheckScreenWidth / 2 + num)));
            num2 = ((Main.rand.Next(2) != 0) ? (num2 + (logicCheckScreenHeight / 2 - num2)) : (num2 - (logicCheckScreenHeight / 2 + num2)));
            num += (int)p.position.X;
            num2 += (int)p.position.Y;
            Vector2 vector = new Vector2(num, num2);
            float num3 = Main.npc[i].position.X - vector.X;
            float num4 = Main.npc[i].position.Y - vector.Y;
            float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
            num5 = 8f / num5;
            num3 *= num5;
            num4 *= num5;
            Projectile.NewProjectile(p.GetSource_ItemUse(Item), num, num2, num4, num5, ModContent.ProjectileType<Projectiles.Melee.PumpkinHead>(), dmg, kb, p.whoAmI, (float)i, 0f);
        }
    }
    */
}
