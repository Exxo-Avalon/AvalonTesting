using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class GlacierStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Glacier Staff");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.damage = 23;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 8;
        Item.rare = ItemRarityID.Blue;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.knockBack = 3.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.GlacierBall>();
        Item.autoReuse = false;
        Item.shootSpeed = 10f;
        Item.value = Item.buyPrice(0, 3, 50, 0);
        Item.UseSound = SoundID.Item43;
        Item.noMelee = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
}
