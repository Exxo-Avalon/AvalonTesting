namespace AvalonTesting.Hooks;

public static class HooksManager
{
    public static void ApplyHooks()
    {
        On.Terraria.Collision.HurtTiles += TrapCollision.OnHurtTiles;
        On.Terraria.Lang.CreateDeathMessage += DeathMessages.OnCreateDeathMessage;
        On.Terraria.Item.IsAPrefixableAccessory += PrefixChanges.OnIsAPrefixableAccessory;
    }
}
