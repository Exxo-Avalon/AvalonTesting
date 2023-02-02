using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Items.Weapons.Ranged;

class testgun : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 75;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Green;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = Item.useAnimation = 6;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.lightRay>();
        Item.shootSpeed = 16f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.scale = 1.3f;
        //Item.holdStyle = 1;
        Item.value = Item.sellPrice(0, 1, 20);
        Item.height = dims.Height;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity = velocity.RotatedByRandom(0.10);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-5, 0);
    }
}
