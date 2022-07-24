using Terraria.ModLoader;

namespace Avalon;
public class AvalonTestingGlobalProjectileInstance : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public bool PiercingUp;
}
