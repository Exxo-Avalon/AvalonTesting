using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class FocusBeam : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Focus Beam");
        Tooltip.SetDefault("Fires a wide-beam laser");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 47;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.shootSpeed = 15f;
        Item.crit += 1;
        Item.mana = 18;
        Item.rare = ItemRarityID.Orange;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 27;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.FocusBeam>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 388500;
        Item.useAnimation = 27;
        Item.height = dims.Height;
        Item.UseSound = new SoundStyle($"{nameof(AvalonTesting)}/Sounds/Item/Beam");
    }
}
