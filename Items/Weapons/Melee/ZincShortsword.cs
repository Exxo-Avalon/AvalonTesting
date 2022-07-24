using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class ZincShortsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Zinc Shortsword");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SilverShortsword);
        Item.damage = 11;
        Item.shootSpeed = 2.1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ZincShortsword>();
        Item.scale = 0.95f;
        Item.value = 4500;
    }
}
