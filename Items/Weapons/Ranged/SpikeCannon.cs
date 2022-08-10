using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class SpikeCannon : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Spike Cannon");
        Tooltip.SetDefault("Uses spikes for ammo\n'You should be careful with this'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 20;
        Item.damage = 65;
        Item.autoReuse = true;
        Item.useTurn = false;
        Item.useAmmo = ItemID.Spike;
        Item.shootSpeed = 14f;
        Item.crit += 2;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.knockBack = 8f;
        Item.useTime = 25;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SpikeCannon>();
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 25;
        Item.UseSound = SoundID.Item11;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10f, 0f);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Shotgun)
            .AddIngredient(ItemID.Spike, 100)
            .AddIngredient(ItemID.SoulofMight, 20)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
