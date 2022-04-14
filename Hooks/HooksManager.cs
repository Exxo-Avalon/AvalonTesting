using IL.Terraria;
using IL.Terraria.GameContent.UI.ResourceSets;
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
        On.Terraria.WorldGen.SmashAltar += EvilAltar.OnSmashAltar;
        ClassicPlayerResourcesDisplaySet.DrawLife += ExtraHealth.ILDrawLife;
        HorizontalBarsPlayerReosurcesDisplaySet.LifeFillingDrawer += ExtraHealth.ILLifeFillingDrawer;
        FancyClassicPlayerResourcesDisplaySet.HeartFillingDrawer += ExtraHealth.ILHeartFillingDrawer;
        On.Terraria.GameContent.UI.ResourceSets.PlayerStatsSnapshot.ctor += ExtraMana.OnPlayerStatsSnapshotCtor;
        ClassicPlayerResourcesDisplaySet.DrawMana += ExtraMana.ILClassicDrawMana;
        FancyClassicPlayerResourcesDisplaySet.StarFillingDrawer += ExtraMana.ILStarFillingDrawer;
        HorizontalBarsPlayerReosurcesDisplaySet.ManaFillingDrawer += ExtraMana.ILManaFillingDrawer;
        //On.Terraria.Item.IsAPrefixableAccessory += PrefixChanges.OnIsAPrefixableAccessory;

        CaesiumBackground.ILCaesium();
    }
}
