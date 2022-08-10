using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class IridiumLongbow : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Iridium Longbow");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 36;
        Item.UseSound = SoundID.Item5;
        Item.damage = 25;
        Item.scale = 1f;
        Item.shootSpeed = 10f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        ; // item.noMelee = true /* tModPorter - this is redundant, for more info see https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide#damage-classes */ ;
        Item.useTime = 16;
        Item.knockBack = 2f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.LightRed;
        Item.value = 25000;
        Item.useAnimation = 16;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.IridiumBar>(), 13)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
            .AddTile(TileID.Anvils).Register();
    }
}
