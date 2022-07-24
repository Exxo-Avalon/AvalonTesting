using System;
using System.Reflection;
using Avalon.Common;
using Avalon.UI;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using On.Terraria;
using On.Terraria.UI;
using Terraria.ModLoader;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class UIChanges : ModHook
{
    protected override void Apply()
    {
        Main.DrawInterface += OnMainDrawInterface;
        IL.Terraria.UI.UserInterface.Update += ILUserInterfaceUpdate;
        Main.DrawInventory += OnMainDrawInventory;
        //IL.Terraria.UI.UIElement.Recalculate += ILUIElementRecalculate;
        IL.Terraria.UI.UIElement.GetClippingRectangle += ILUIElementGetClippingRectangle;
        UIElement.Remove += OnUIElementRemove;
    }

    // private static readonly float characterUIStaminaYOffset = 30f;
    // private static readonly float characterUIStaminaPanelWidth = 200f;
    // // Adds proper text alpha support
    // public static Vector2 OnDrawColorCodedStringWithShadow(On.Terraria.UI.Chat.ChatManager.orig_DrawColorCodedStringWithShadow_SpriteBatch_DynamicSpriteFont_string_Vector2_Color_float_Vector2_Vector2_float_float orig,
    //                                                        SpriteBatch spriteBatch, object font, string text, Vector2 position, Color baseColor, float rotation, Vector2 origin, Vector2 baseScale, float maxWidth, float spread)
    // {
    //     TextSnippet[] snippets = ChatManager.ParseMessage(text, baseColor).ToArray();
    //     ChatManager.ConvertNormalSnippets(snippets);
    //     ChatManager.DrawColorCodedStringShadow(spriteBatch, (DynamicSpriteFont)font, snippets, position, new Color(0, 0, 0, baseColor.A), rotation, origin, baseScale, maxWidth, spread);
    //     return ChatManager.DrawColorCodedString(spriteBatch, (DynamicSpriteFont)font, snippets, position, new Color(255, 255, 255, baseColor.A), rotation, origin, baseScale, out _, maxWidth);
    //     DynamicSpriteFontExtensionMethods.DrawString();
    // }
    //
    // public static void ILUICharacterListItemDrawSelf(ILContext il)
    // {
    //     var c = new ILCursor(il);
    //
    //     FieldReference dataField = null;
    //     MethodReference getPlayer = null;
    //     MethodReference drawPanel = null;
    //
    //     if (!c.TryGotoNext(i => i.MatchLdsfld<Main>(nameof(Main.heartTexture))))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoNext(i => i.MatchLdarg(1)))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoNext(i => i.MatchLdfld(out dataField)))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoNext(i => i.MatchCallvirt(out getPlayer)))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoPrev(i => i.MatchLdsfld<Main>(nameof(Main.heartTexture))))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoPrev(i => i.MatchLdarg(1)))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoPrev(i => i.MatchCall(out drawPanel)))
    //     {
    //         return;
    //     }
    //
    //     if (!c.TryGotoNext(i => i.MatchLdarg(1)))
    //     {
    //         return;
    //     }
    //
    //     #region lazyfix for panel size height
    //     c.Emit(OpCodes.Ldarg_0);
    //     c.Emit(OpCodes.Ldarg_1);
    //     c.Emit(OpCodes.Ldloc, 6);
    //     c.EmitDelegate<Func<Vector2, Vector2>>((vector2) =>
    //     {
    //         return vector2 + new Vector2(0f, characterUIStaminaYOffset);
    //     });
    //     c.EmitDelegate<Func<float>>(() =>
    //     {
    //         return characterUIStaminaPanelWidth;
    //     });
    //     c.Emit(OpCodes.Call, drawPanel);
    //
    //     c.Emit(OpCodes.Ldarg_0);
    //     c.Emit(OpCodes.Ldarg_1);
    //     c.Emit(OpCodes.Ldloc, 6);
    //     c.EmitDelegate<Func<Vector2, Vector2>>((vector2) =>
    //     {
    //         return vector2 + new Vector2(0f, characterUIStaminaYOffset - 10f);
    //     });
    //     c.EmitDelegate<Func<float>>(() =>
    //     {
    //         return characterUIStaminaPanelWidth;
    //     });
    //     c.Emit(OpCodes.Call, drawPanel);
    //     #endregion
    //
    //     c.Emit(OpCodes.Ldarg_1);
    //     c.Emit(OpCodes.Ldloc, 6);
    //     c.Emit(OpCodes.Ldarg_0);
    //     c.Emit(OpCodes.Ldfld, dataField);
    //     c.Emit(OpCodes.Callvirt, getPlayer);
    //
    //     c.EmitDelegate<Action<SpriteBatch, Vector2, Player>>((spriteBatch, vector2, player) =>
    //     {
    //         AvalonTestingModPlayer modPlayer = player.Avalon();
    //         if (modPlayer == null)
    //         {
    //             return;
    //         }
    //
    //         Texture2D staminaTexture = AvalonTesting.Mod.Assets.Request<Texture2D>("Sprites/Stamina").Value;
    //         Texture2D defenceTexture = AvalonTesting.Mod.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Defense");
    //         //switch ((modPlayer.statStamMax - 1) / 150)
    //         //{
    //         //    case 0:
    //         //        staminaTexture = AvalonTesting.mod.GetTexture("Sprites/Stamina");
    //         //        break;
    //         //    case 1:
    //         //        staminaTexture = AvalonTesting.mod.GetTexture("Sprites/Stamina2");
    //         //        break;
    //         //    case 2:
    //         //        staminaTexture = AvalonTesting.mod.GetTexture("Sprites/Stamina3");
    //         //        break;
    //         //    default:
    //         //        staminaTexture = AvalonTesting.mod.GetTexture("Sprites/Stamina");
    //         //        break;
    //         //}
    //
    //         player.ResetEffects();
    //         modPlayer.UpdateMana();
    //         player.UpdateEquips(Main.myPlayer);
    //         player.UpdateArmorSets(Main.myPlayer);
    //
    //         spriteBatch.Draw(staminaTexture, vector2 + new Vector2(5f, 2f + characterUIStaminaYOffset), Color.White);
    //         vector2.X += 10f + Main.heartTexture.Width;
    //         Terraria.Utils.DrawBorderString(spriteBatch, modPlayer.statStamMax2 + " SP", vector2 + new Vector2(0f, 3f + characterUIStaminaYOffset), Color.White);
    //
    //         vector2.X += 65f;
    //         spriteBatch.Draw(defenceTexture, vector2 + new Vector2(5f, characterUIStaminaYOffset), new Rectangle(3, 3, 26, 26), Color.White);
    //         vector2.X += 10f + Main.manaTexture.Width;
    //         Terraria.Utils.DrawBorderString(spriteBatch, player.statDefense + " DP", vector2 + new Vector2(0f, 3f + characterUIStaminaYOffset), Color.White);
    //     });
    // }
    // public static void ILUICharacterListItemCtor(ILContext il)
    // {
    //     var c = new ILCursor(il);
    //
    //     if (!c.TryGotoNext(i => i.MatchLdcR4(96)))
    //     {
    //         return;
    //     }
    //
    //     c.Index++;
    //     c.EmitDelegate<Func<float, float>>((origHeight) =>
    //     {
    //         return origHeight + characterUIStaminaYOffset - 2f;
    //     });
    // }

    private static void OnMainDrawInterface(Main.orig_DrawInterface orig, Terraria.Main self, GameTime gameTime)
    {
        AvalonTesting.Mod.CheckPointer = true;
        orig(self, gameTime);
    }

    private static void ILUserInterfaceUpdate(ILContext il)
    {
        var c = new ILCursor(il);
        c.GotoNext(i => i.MatchStloc(5));

        c.EmitDelegate<Func<Terraria.UI.UIElement?, Terraria.UI.UIElement?>>(uiElement =>
        {
            if (Terraria.Main.gameMenu || uiElement is Terraria.UI.UIState)
            {
                return uiElement;
            }

            if (!AvalonTesting.Mod.CheckPointer || uiElement == null)
            {
                return null;
            }

            AvalonTesting.Mod.CheckPointer = false;
            return uiElement;
        });
    }

    /// <summary>
    ///     Some trickery that changes default behaviour of Recalculate to only recalculate self when element is ExxoUIElement.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>

    /*private static void ILUIElementRecalculate(ILContext il)
    {
        var c = new ILCursor(il);
        c.GotoNext(i => i.MatchCallvirt(typeof(Terraria.UI.UIElement).GetMethod(
            nameof(Terraria.UI.UIElement.RecalculateChildren),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, Type.EmptyTypes, null)));
        c.Index--;

        ILLabel label = c.DefineLabel();

        c.Emit(OpCodes.Ldarg_0);
        c.EmitDelegate<Func<Terraria.UI.UIElement, bool>>(element => element is ExxoUIElement);
        c.Emit(OpCodes.Brtrue, label)
            .GotoNext(i => i.MatchRet())
            .MarkLabel(label);
    }*/

    //Hey lion here, for some reason this causes the mod to fail to load. If someone with higher knowledge on IL edits could have a look that would be good

    /// <summary>
    ///     Fixes vanilla's decision to clamp the clipping rectangle from 0 to the screen width which doesnt respect the
    ///     position of the element... smh.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>
    private static void ILUIElementGetClippingRectangle(ILContext il)
    {
        var c = new ILCursor(il);
        c.GotoNext(i => i.MatchRet())
            .Emit(OpCodes.Pop)
            .Emit(OpCodes.Ldloc_2);
    }

    /// <summary>
    ///     ExxoUIElements use a better removal system that removes children after updating, this allows children to remove
    ///     themselves during update.
    /// </summary>
    /// <param name="orig">Delegate that calls the original method.</param>
    /// <param name="self">The calling UIElement instance.</param>
    private static void OnUIElementRemove(UIElement.orig_Remove orig, Terraria.UI.UIElement self)
    {
        if (self.Parent is ExxoUIElement exxoParent)
        {
            exxoParent.ElementsForRemoval.Enqueue(self);
        }
        else
        {
            orig(self);
        }
    }

    private static void OnMainDrawInventory(Main.orig_DrawInventory orig, Terraria.Main self)
    {
        Vector2 oldMouseScreen = Terraria.Main.MouseScreen;
        if (!AvalonTesting.Mod.CheckPointer)
        {
            Terraria.Main.mouseX = -100;
            Terraria.Main.mouseY = -100;
        }

        orig(self);
        if (!AvalonTesting.Mod.CheckPointer)
        {
            Terraria.Main.mouseX = (int)oldMouseScreen.X;
            Terraria.Main.mouseY = (int)oldMouseScreen.Y;
        }
    }
}
