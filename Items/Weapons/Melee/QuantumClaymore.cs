using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Melee;
public class QuantumClaymore : ModItem
{
    int DontShootEverySwingkthxbye = 0;
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Quantum Claymore");
        Tooltip.SetDefault("'Tear through time to tear apart your foes'\n[c/C39FDD:10th Anniversary Contest Winner - Waasephi]");
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
        Item.scale = 1.5f;
        Item.shootSpeed = 10;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.QuantumBeam>();
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
        Vector2 SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly = player.position - new Vector2(10,10).RotatedBy(Main.rand.NextFloat(-0.3926991f, 0.3926991f));
        for (int i = 0; i < 3; i++)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly, SwordSpawnFunnyPlaceRealOnGodTheyComeFromHereQuiteCoolHonestly.DirectionTo(Main.MouseWorld) * (Item.shootSpeed * Main.rand.NextFloat(0.6f, 1.3f)), ModContent.ProjectileType<Projectiles.Melee.QuantumBeam2>(), (int)(Item.damage * 0.8f), Item.knockBack * 0.1f, player.whoAmI);
        }
    }
}
