using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;

class NickelShortsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Nickel Shortsword");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 9;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.width = dims.Width;
        Item.useTime = 11;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NickelShortsword>();
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.value = 1800;
        Item.useAnimation = 11;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}