using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class VertexofExcalibur : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vertex of Excalibur");
        Tooltip.SetDefault("Deals more damage to enemies affected by a debuff\n'The unification of dark and light'");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 90;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 18;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 9, 63, 0);
        Item.useAnimation = 18;
        Item.reuseDelay = 2;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.VertexSlash>();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        //Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<Projectiles.Melee.VertexSlash2>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        Projectile.NewProjectile(source, position, new Vector2(player.direction, 0), ModContent.ProjectileType<Projectiles.Melee.VertexSlash>(), damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
        return false;
    }
    //public override void MeleeEffects(Player player, Rectangle hitbox)
    //{
    //    if (Main.rand.NextBool(3))
    //    {
    //        int num313 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink);
    //        Main.dust[num313].noGravity = true;
    //        Main.dust[num313].fadeIn = 1.25f;
    //        Main.dust[num313].velocity *= 0.25f;
    //    }
    //}
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.NightsEdge)
            .AddIngredient(ItemID.Excalibur)
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.LightShard)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.BeetleBar>(), 4)
            .AddTile(TileID.AdamantiteForge).Register();
    }
    //public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    //{
    //    int debuffCount = 0;
    //    for (int i = 0; i < target.buffType.Length; i++)
    //    {
    //        if (Main.debuff[target.buffType[i]])
    //        {
    //            debuffCount++;
    //        }
    //    }
    //    if (debuffCount > 0)
    //    {
    //        if (target.boss)
    //        {
    //            damage = (int)(damage * 1.2 * debuffCount);
    //        }
    //        else
    //        {
    //            damage = (int)(damage * 1.45 * debuffCount);
    //        }
    //    }


    //bool hasDebuff = false;
    //for (int i = 0; i < target.buffType.Length; i++)
    //{
    //    if (Main.debuff[target.buffType[i]])
    //    {
    //        hasDebuff = true;
    //        break;
    //    }
    //}
    //if (hasDebuff)
    //{
    //    if (target.boss) damage = (int)(damage * 1.35);
    //    else damage = (int)(damage * 1.65);
    //}
}
