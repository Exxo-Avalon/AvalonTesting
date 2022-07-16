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
        Item.CloneDefaults(ItemID.IronShortsword);
        Item.damage = 9;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NickelShortsword>();
        Item.scale = 1f;
        Item.value = 1800;
    }
}
