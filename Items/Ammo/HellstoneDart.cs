using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class HellstoneDart : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hellstone Dart");
        Tooltip.SetDefault("For use with Blowpipes");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 9;
        Item.consumable = true;
        Item.ammo = AmmoID.Dart;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.shoot = ModContent.ProjectileType<Projectiles.HellstoneSeed>();
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
}
