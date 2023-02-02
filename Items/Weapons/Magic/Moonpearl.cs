using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic
{
    class Moonpearl : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonpearl");
            Tooltip.SetDefault("[c/C99DFF:Strengthens at night]");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Rectangle dims = this.GetDims();
            Item.autoReuse = true;
            Item.width = dims.Width;
            Item.height = dims.Height;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.damage = 9;
            Item.mana = 6;
            Item.rare = ItemRarityID.Pink;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.knockBack = 2f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Moonlight>();
            Item.shootSpeed = 12f;
            Item.value = 20000;
            Item.UseSound = SoundID.Item43;
        }
        public override Vector2? HoldoutOrigin()
        {
            return new Vector2(10, 10);
        }
        public override float UseTimeMultiplier(Player player)
        {
            if (!Main.dayTime)
            {
                return 1f;
            }
            else
            {
                return 1.5f;
            }
        }
        public override float UseAnimationMultiplier(Player player)
        {
            if (!Main.dayTime)
            {
                return 1f;
            }
            else
            {
                return 1.5f;
            }
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (Main.dayTime)
            {
                damage = new StatModifier(0f, 2f);
            }
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Main.dayTime)
            {
                //type = ModContent.ProjectileType<Projectiles.MoonlightWeak>();
            }
            if (!Main.dayTime)
            {
                type = ModContent.ProjectileType<Projectiles.Magic.Moonlight>();
            }
            Vector2 pos = player.Center + new Vector2(35, 0).RotatedBy(player.AngleTo(Main.MouseWorld));

            position.X = pos.X;
            position.Y = pos.Y;

            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
            velocity.X = perturbedSpeed.X;
            velocity.Y = perturbedSpeed.Y;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (!Main.dayTime)
            {
                return new Color(255, 255, 255, 150);
            }
            return lightColor;
        }
    }
}
