using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class TrueAeonsEternity : ModItem
{
    int swingCounter = 0;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("True Aeon's Eternity");
        Tooltip.SetDefault("Fires a burst of stars every six swings");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 63;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
        Item.scale = 1.1f;
        Item.shootSpeed = 11f;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 35;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.AeonBeam>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
        swingCounter++;
        if (swingCounter >= 6)
        {
            for (int num185 = 0; num185 < 6; num185++)
            {
                float num186 = velocity.X;
                float num187 = velocity.Y;
                num186 += (float)Main.rand.Next(-40, 41) * 0.05f;
                num187 += (float)Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, num186, num187, ModContent.ProjectileType<Projectiles.Melee.AeonStar>(), damage, knockback, player.whoAmI, 0f, 0f);
            }
            swingCounter = 0;
        }
        return false;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
            int num208 = Main.rand.Next(3);
            if (num208 == 0)
            {
                num208 = 15;
            }
            else if (num208 == 1)
            {
                num208 = 57;
            }
            else if (num208 == 2)
            {
                num208 = 58;
            }
            int num209 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, num208);
            Dust dust = Main.dust[num209];
            dust.velocity *= 0.2f;
            dust.scale = 1.3f;
        }
    }
}
