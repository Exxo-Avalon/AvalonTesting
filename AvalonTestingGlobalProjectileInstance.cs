using Terraria.ModLoader;

namespace AvalonTesting;
public class AvalonTestingGlobalProjectileInstance : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public bool PiercingUp;
}
