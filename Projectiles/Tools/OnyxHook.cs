using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Tools;

public class OnyxHook : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Onyx Hook");
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
    }

    public override float GrappleRange()
    {
        return Main.player[Projectile.owner].GetModPlayer<Players.ExxoPlayer>().HookBonus ? 560f : 450f;
    }

    public override void NumGrappleHooks(Player player, ref int numHooks)
    {
        numHooks = 1;
    }

}
