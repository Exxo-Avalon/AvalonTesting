using IL.Terraria;
using IL.Terraria.GameContent.UI.ResourceSets;
using IL.Terraria.GameContent.UI.States;
using IL.Terraria.UI;
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
        WorldGen.GenerateWorld += GenPasses.ILGenerateWorld;
        GenPasses.Hook_GenPassReset += ContagionWorldGen.ILGenPassReset;
        GenPasses.Hook_GenPassShinies += WorldGenEdits.ILGenPassShinies;
        GenPasses.Hook_GenPassAltars += ContagionWorldGen.ILGenPassAltars;
        UIWorldCreation.MakeInfoMenu += UIWorldCreationEdits.ILMakeInfoMenu;
        UIElement.Draw += UIChanges.ILUIElementDraw;
        UIChanges.Hook_OnSpriteFontInternalDraw += UIChanges.OnSpriteFontInternalDraw;
        On.Terraria.Main.DrawInterface += UIChanges.OnMainDrawInterface;
        UserInterface.Update += UIChanges.ILUserInterfaceUpdate;
        //UIElement.GetElementAt += UIChanges.ILUIElementGetElementAt;
        UIElement.Recalculate += UIChanges.ILUIElementRecalculate;
        UIElement.GetClippingRectangle += UIChanges.ILUIElementGetClippingRectangle;
        On.Terraria.UI.UIElement.Remove += UIChanges.OnUIElementRemove;
        On.Terraria.Main.DrawInventory += UIChanges.OnMainDrawInventory;
        On.Terraria.GameContent.UI.States.UIWorldCreation.AddWorldEvilOptions +=
            UIWorldCreationEdits.OnAddWorldEvilOptions;
        //On.Terraria.Item.IsAPrefixableAccessory += PrefixChanges.OnIsAPrefixableAccessory;

        CaesiumBackground.ILCaesium();
    }
}
