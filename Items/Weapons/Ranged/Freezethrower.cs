using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Ranged;

internal class Freezethrower : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Freezethrower");
        Tooltip.SetDefault("Uses gel for ammo");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item34;
        Item.damage = 70;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Gel;
        Item.shootSpeed = 8.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.useTime = 5;
        Item.knockBack = 0.625f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Freezethrower>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1000000;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
