namespace AvalonTesting.Hooks;

public static class HooksManager
{
    public static void ApplyHooks()
    {
        On.Terraria.Collision.HurtTiles += TrapCollision.OnHurtTiles;
    }
}
