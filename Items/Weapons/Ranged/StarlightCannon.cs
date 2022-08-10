using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

public class StarlightCannon : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Starlight Cannon");
        Tooltip.SetDefault("Fires large balls of star matter\nCharges for 2.5 seconds");
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.damage = 350;
        Item.scale = 1.1f;
        Item.DamageType = DamageClass.Magic;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.rare = ItemRarityID.Lime;
        Item.noMelee = true;
        Item.channel = true;
        Item.mana = 50;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.ChargingStar>();
        Item.shootSpeed = 6f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Charging");
        Item.value = 15500000;
    }

}
