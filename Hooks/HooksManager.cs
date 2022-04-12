using IL.Terraria;
using On.Terraria.GameContent.ItemDropRules;

namespace AvalonTesting.Hooks;

public static class HooksManager
{
    public static void ApplyHooks()
    {
        Player.IsInInteractionRangeToMultiTileHitbox += DresserFix.ILIsInInteractionRangeToMultiTileHitbox;
        On.Terraria.Collision.HurtTiles += TrapCollision.OnHurtTiles;
        On.Terraria.Lang.CreateDeathMessage += DeathMessages.OnCreateDeathMessage;
        On.Terraria.Projectile.FishingCheck_RollDropLevels += BuffEffects.OnFishingCheck_RollDropLevels;
        CommonCode.DropItemForEachInteractingPlayerOnThePlayer +=
            BuffEffects.OnDropItemForEachInteractingPlayerOnThePlayer;
        On.Terraria.Player.OpenBossBag += BossBagDrops.OnOpenBossBag;
        //On.Terraria.Item.IsAPrefixableAccessory += PrefixChanges.OnIsAPrefixableAccessory;
    }
}
