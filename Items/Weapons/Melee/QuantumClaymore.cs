using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;
public class QuantumClaymore : ModItem
{
    int DontShootEverySwingkthxbye = 0;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quantum Claymore");
        Tooltip.SetDefault("'Tear through time to rip apart your foes'\nFires a piercing quantum beam\nTrue melee strikes summon extra non-piercing beams\n[c/C39FDD:10th Anniversary Contest Winner - Waasephi]");
        SacrificeTotal = 1;
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 88;
        Item.autoReuse = true;
        Item.rare = ModContent.RarityType<Rarities.QuantumRarity>();
        Item.width = dims.Width;
        Item.knockBack = 10f;
        Item.useTime = 17;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10, 90, 0);
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item15;
        Item.scale = 1.2f;
        Item.shootSpeed = 10;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.QuantumBeam>();
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CorruptedBar>(), 10)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.Ectoplasm, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        DontShootEverySwingkthxbye++;
        if (DontShootEverySwingkthxbye > 1)
        {
            DontShootEverySwingkthxbye = 0;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback * 0.1f, player.whoAmI);
        }
        return false;
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        for (int i = 0; i < Main.rand.Next(3, 4); i++)
        {
            Vector2 SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly = player.position - new Vector2(Main.rand.Next(230,280) * player.direction, Main.rand.Next(-75, 75));

            Projectile.NewProjectile(player.GetSource_FromThis(), SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly, SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly.DirectionTo(Main.MouseWorld) * (Item.shootSpeed * Main.rand.NextFloat(1.6f, 3f)), ModContent.ProjectileType<Projectiles.Melee.QuantumBeam2>(), (int)(Item.damage * 0.6f), Item.knockBack * 0.1f, player.whoAmI);
        }
    }
}
