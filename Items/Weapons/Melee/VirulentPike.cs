using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class VirulentPike : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Pike");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 38;
        Item.damage = 18;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 3.6f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.knockBack = 2.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.VirulentPike>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 36, 0);
    }
}
