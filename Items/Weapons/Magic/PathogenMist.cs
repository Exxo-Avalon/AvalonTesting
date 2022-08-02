using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class PathogenMist : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pathogen Mist");
        Tooltip.SetDefault("Fires a blast of infected mist");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 28;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 10f;
        Item.mana = 5;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.knockBack = 1.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.VirulentCloud>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 500000;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public SoundStyle gas = new SoundStyle("Terraria/Sounds/Item_34")
    {
        Volume = 0.5f,
        Pitch = -0.5f,
        PitchVariance = 1.5f,
        MaxInstances = 10,
    };
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        type = Main.rand.Next(new int[] { ModContent.ProjectileType<Projectiles.Melee.VirulentCloudWeak>(), ModContent.ProjectileType<Projectiles.Melee.VirulentCloudSmallWeak>() });
        SoundEngine.PlaySound(gas, player.Center);
        for (int num194 = 0; num194 < 2; num194++)
        {
            float num195 = velocity.X;
            float num196 = velocity.Y;
            num195 += Main.rand.Next(-40, 41) * 0.05f;
            num196 += Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num195, num196, type, damage, knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
