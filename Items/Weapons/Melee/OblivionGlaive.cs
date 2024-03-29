using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class OblivionGlaive : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Oblivion Glaive");
        Tooltip.SetDefault("Striking an enemy causes shadow glaives to rain down");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 46;
        Item.height = 50;
        Item.damage = 120;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 5f;
        Item.rare = ModContent.RarityType<DarkRedRarity>();
        Item.noMelee = true;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.OblivionGlaive>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 20, 0, 0);
        Item.UseSound = SoundID.Item1;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
