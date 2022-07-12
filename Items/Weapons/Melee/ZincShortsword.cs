using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class ZincShortsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Zinc Shortsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 11;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ZincShortsword>();
        Item.value = 4500;
        Item.useAnimation = 10;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}