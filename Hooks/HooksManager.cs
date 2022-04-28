using IL.Terraria;
using IL.Terraria.GameContent.UI.ResourceSets;
using IL.Terraria.GameContent.UI.States;
using IL.Terraria.UI;
using On.Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace AvalonTesting.Hooks;

public static class HooksManager
{
    public static void ApplyHooks()
    {
        // ----------- Server/Client ----------- //
        Player.IsInInteractionRangeToMultiTileHitbox += DresserFix.ILIsInInteractionRangeToMultiTileHitbox;
        On.Terraria.Collision.HurtTiles += TrapCollision.OnHurtTiles;
        On.Terraria.Lang.CreateDeathMessage += DeathMessages.OnCreateDeathMessage;
        On.Terraria.Projectile.FishingCheck_RollDropLevels += BuffEffects.OnFishingCheck_RollDropLevels;
        CommonCode.DropItemForEachInteractingPlayerOnThePlayer +=
            BuffEffects.OnDropItemForEachInteractingPlayerOnThePlayer;
        On.Terraria.Player.OpenBossBag += BossBagDrops.OnOpenBossBag;
        On.Terraria.WorldGen.SmashAltar += EvilAltar.OnSmashAltar;
        WorldGen.GenerateWorld += GenPasses.ILGenerateWorld;
        GenPasses.Hook_GenPassReset += ContagionWorldGen.ILGenPassReset;
        GenPasses.Hook_GenPassShinies += WorldGenEdits.ILGenPassShinies;
        GenPasses.Hook_GenPassAltars += ContagionWorldGen.ILGenPassAltars;

        if (Terraria.Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        ClassicPlayerResourcesDisplaySet.DrawLife += ExtraHealth.ILDrawLife;
        HorizontalBarsPlayerReosurcesDisplaySet.LifeFillingDrawer += ExtraHealth.ILLifeFillingDrawer;
        FancyClassicPlayerResourcesDisplaySet.HeartFillingDrawer += ExtraHealth.ILHeartFillingDrawer;
        On.Terraria.GameContent.UI.ResourceSets.PlayerStatsSnapshot.ctor += ExtraMana.OnPlayerStatsSnapshotCtor;
        ClassicPlayerResourcesDisplaySet.DrawMana += ExtraMana.ILClassicDrawMana;
        FancyClassicPlayerResourcesDisplaySet.StarFillingDrawer += ExtraMana.ILStarFillingDrawer;
        HorizontalBarsPlayerReosurcesDisplaySet.ManaFillingDrawer += ExtraMana.ILManaFillingDrawer;
        UIWorldCreation.MakeInfoMenu += UIWorldCreationEdits.ILMakeInfoMenu;
        UIElement.Draw += UIChanges.ILUIElementDraw;
        UIChanges.Hook_OnSpriteFontInternalDraw += UIChanges.OnSpriteFontInternalDraw;
        On.Terraria.Main.DrawInterface += UIChanges.OnMainDrawInterface;
        UserInterface.Update += UIChanges.ILUserInterfaceUpdate;
        On.Terraria.Main.DrawInventory += UIChanges.OnMainDrawInventory;
        UIElement.Recalculate += UIChanges.ILUIElementRecalculate;
        UIElement.GetClippingRectangle += UIChanges.ILUIElementGetClippingRectangle;
        On.Terraria.UI.UIElement.Remove += UIChanges.OnUIElementRemove;

        On.Terraria.GameContent.UI.States.UIWorldCreation.AddWorldEvilOptions +=
            UIWorldCreationEdits.OnAddWorldEvilOptions;

        UIWorldCreation.FinishCreatingWorld += UIWorldCreationEdits.ILFinishCreatingWorld;
        UIWorldCreation.UpdatePreviewPlate += UIWorldCreationEdits.ILUpdatePreviewPlate;

        CaesiumBackground.ILCaesium();
    }
}
