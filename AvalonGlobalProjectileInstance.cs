using Terraria.ModLoader;

namespace Avalon;
public class AvalonGlobalProjectileInstance : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    public bool PiercingUp;
}
