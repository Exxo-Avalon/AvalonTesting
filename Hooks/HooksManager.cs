using On.Terraria;
using On.Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.Hooks;

public static class HooksManager
{
    public static void ApplyHooks()
    {
        Collision.HurtTiles += TrapCollision.OnHurtTiles;
        Lang.CreateDeathMessage += DeathMessages.OnCreateDeathMessage;
        Projectile.FishingCheck_RollDropLevels += BuffEffects.OnFishingCheck_RollDropLevels;
        CommonCode.DropItemForEachInteractingPlayerOnThePlayer +=
            BuffEffects.OnDropItemForEachInteractingPlayerOnThePlayer;
        Player.OpenBossBag += BossBagDrops.OnOpenBossBag;
        //On.Terraria.Item.IsAPrefixableAccessory += PrefixChanges.OnIsAPrefixableAccessory;
    }
}
