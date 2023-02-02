using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class testsword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bismuth Broadsword");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 16;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.width = dims.Width;
        Item.useTime = 60;
        Item.knockBack = 5.2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.meleeSlash>();
        Item.shootSpeed = 0f;
        Item.value = 12000;
        Item.useAnimation = 60;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
}
