using Terraria.ID;

namespace AvalonTesting.Data.Sets;

public static class Projectile
{
    public static readonly bool[] MinionProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.HornetStinger,
        ProjectileID.ImpFireball,
        ProjectileID.MiniRetinaLaser,
        ProjectileID.PygmySpear,
        ProjectileID.UFOLaser,
        ProjectileID.MiniSharkron,
        ProjectileID.StardustCellMinionShot
    );
}
