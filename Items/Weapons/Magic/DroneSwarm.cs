using Avalon.Items.Material;
using Avalon.Items.Placeable.Bar;
using Avalon.Items.Placeable.Tile;
using Avalon.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class DroneSwarm : ModItem
{
    private byte soundCounter;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Drone Swarm");
        Tooltip.SetDefault("Casts a spread of three homing drones\n'Thousands of bright yellow...!'");
        SacrificeTotal = 1;
        Item.staff[Type] = true;
    }
    public override Vector2? HoldoutOrigin() => new Vector2(10f, 10f);
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 25;
        Item.autoReuse = true;
        Item.scale = 1.2f;
        Item.shootSpeed = 12f;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 8;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Drone>();
        Item.DamageType = DamageClass.Magic;
        Item.mana = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(gold: 5);
        Item.useAnimation = 8;
        Item.height = dims.Height;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 255);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        soundCounter++;
        for (int num194 = 0; num194 < 3; num194++)
        {
            float num195 = velocity.X;
            float num196 = velocity.Y;
            num195 += Main.rand.Next(-40, 41) * 0.05f;
            num196 += Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num195, num196, type, damage, knockback, player.whoAmI, 0f, 0f);
        }
        if (soundCounter > 3)
        {
            SoundEngine.PlaySound(SoundID.Item43, position);
            soundCounter = 0;
        }
        return false;
    }
}
