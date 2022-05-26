using AvalonTesting.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Data.Sets;

public static class Projectile
{
    public static readonly bool[] DontReflect = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.Stinger,
        ProjectileID.RainCloudMoving,
        ProjectileID.RainCloudRaining,
        ProjectileID.BloodCloudMoving,
        ProjectileID.BloodCloudRaining,
        ProjectileID.FrostHydra,
        ProjectileID.InfernoFriendlyBolt,
        ProjectileID.InfernoFriendlyBlast,
        ProjectileID.PhantasmalDeathray,
        ProjectileID.FlyingPiggyBank,
        ProjectileID.Glowstick,
        ProjectileID.BouncyGlowstick,
        ProjectileID.SpelunkerGlowstick,
        ProjectileID.StickyGlowstick,
        ProjectileID.WaterGun,
        ProjectileID.SlimeGun,
        ModContent.ProjectileType<Ghostflame>(),
        ModContent.ProjectileType<WallofSteelLaser>(),
        ModContent.ProjectileType<ElectricBolt>(),
        ModContent.ProjectileType<HomingRocket>(),
        ModContent.ProjectileType<StingerLaser>(),
        ModContent.ProjectileType<CaesiumFireball>(),
        ModContent.ProjectileType<CaesiumCrystal>(),
        ModContent.ProjectileType<CaesiumGas>(),
        ModContent.ProjectileType<SpikyBall>(),
        ModContent.ProjectileType<Spike>(),
        ModContent.ProjectileType<CrystalShard>(),
        ModContent.ProjectileType<WallofSteelLaserEnd>(),
        ModContent.ProjectileType<WallofSteelLaserStart>(),
        ModContent.ProjectileType<CrystalBit>(),
        ModContent.ProjectileType<CrystalBeam>());

    public static readonly bool[] MinionProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.HornetStinger,
        ProjectileID.ImpFireball,
        ProjectileID.MiniRetinaLaser,
        ProjectileID.PygmySpear,
        ProjectileID.UFOLaser,
        ProjectileID.MiniSharkron,
        ProjectileID.StardustCellMinionShot);
}
