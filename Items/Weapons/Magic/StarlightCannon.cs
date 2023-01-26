using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

public class StarlightCannon : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Starlight Cannon");
        Tooltip.SetDefault("Charges up energy to fire balls of star matter\n'Wait, what do you mean overch-'");
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.damage = 350;
        Item.DamageType = DamageClass.Magic;
        Item.autoReuse = false;
        Item.useTurn = true;
        Item.rare = ItemRarityID.Lime;
        Item.noMelee = true;
        Item.channel = true;
        Item.noUseGraphic= true;
        Item.mana = 10;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.ChargingStar>();
        Item.shootSpeed = 0f;
        //Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/Charging");
        Item.value = 15500000;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8, -6);
    }

}
