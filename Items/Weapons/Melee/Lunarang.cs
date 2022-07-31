using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

internal class Lunarang : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Lunarang");
        Tooltip.SetDefault("Empowers at night");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 35;
        Item.autoReuse = true;
        Item.shootSpeed = 10f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Lunarang>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noUseGraphic = true;
        Item.value = Item.sellPrice(0, 20);
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
        if (!Main.dayTime)
        {
            damage += 0.5f;
        }
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return lightColor;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Shroomerang, 1)
            .AddIngredient(ItemID.EnchantedBoomerang, 1)
            .AddIngredient(ItemID.IceBoomerang, 1)
            .AddIngredient(ItemID.Flamarang, 1)
            .AddTile(TileID.DemonAltar).Register();
    }
}
