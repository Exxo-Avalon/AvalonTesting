using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class TritanoriumBroadsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tritanorium Broadsword");
        SacrificeTotal = 1;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 42;
        Item.damage = 110;
        Item.autoReuse = true;
        Item.scale = 1.2f;
        Item.rare = ModContent.RarityType<MagentaRarity>();
        Item.useTime = 16;
        Item.knockBack = 15f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.useAnimation = 16;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TritonWave>();
        Item.shootSpeed = 20;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(2) == 0)
        {
            int num162 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.TritanoriumFlame>(), 0f, 0f, 0, default(Color), 2f);
            Main.dust[num162].noGravity = true;
        }
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(30));
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
        }
        return false;
    }
}
