using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Items.Weapons.Ranged;

class Cherrybomb : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Firecracker");
        //Tooltip.SetDefault("");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 75;
        Item.autoReuse = false;
        Item.shootSpeed = 14f;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Green;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 100;
        Item.knockBack = 4f;
        //Item.useAmmo = ModContent.ItemType<Ammo.Firework>();
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.CherrybombRocket>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.holdStyle = 3;
        Item.value = Item.sellPrice(0, 1, 20);
        Item.useAnimation = 100;
        Item.height = dims.Height;
        //Item.UseSound = SoundID.Item11;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-12, -6);
    }
    public SoundStyle shoot = new SoundStyle("Terraria/Sounds/Item_11")
    {
        Volume = 1.2f,
        Pitch = -0.5f,
        PitchVariance = 0f,
        MaxInstances = 10,
    };
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        SoundEngine.PlaySound(shoot, player.Center);

        player.velocity += Vector2.Normalize(player.Center - Main.MouseWorld) * 7.5f;

        Vector2 pos = player.Center + new Vector2(10, -8 * player.direction).RotatedBy(player.AngleTo(Main.MouseWorld));

        position.X = pos.X;
        position.Y = pos.Y;
    }
}
