using System;
using System.Linq;
using System.Reflection;
using AvalonTesting.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AvalonTesting.Hooks;

public static class UIWorldCreationEdits
{
    public enum MenuEvilOption
    {
        Random,
        Corruption,
        Crimson,
        Contagion
    }

    public static Func<UIWorldCreation, UIText> GetDescriptionUIText =
        Utilities.CreatePropertyOrFieldReaderDelegate<UIWorldCreation, UIText>("_descriptionText");

    private static ExxoUIListGrid EvilListGrid;
    private static MenuEvilOption CurrentEvilOption;

    public static void ILMakeInfoMenu(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdstr("evil")))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdcR4(out _)))
        {
            return;
        }

        c.Remove();
        c.EmitDelegate(() => EvilListGrid.MinHeight.Pixels + 12);
    }

    public static void OnAddWorldEvilOptions(
        On.Terraria.GameContent.UI.States.UIWorldCreation.orig_AddWorldEvilOptions orig,
        UIWorldCreation self, UIElement container,
        float accumulatedHeight,
        UIElement.MouseEvent clickEvent,
        string tagGroup, float usableWidthPercent)
    {
        orig(self, container, accumulatedHeight, clickEvent, tagGroup, usableWidthPercent);

        // Remove last 3 elements (original evil buttons)
        UIElement[] tempArray = container.Children.ToArray();
        for (int i = tempArray.Length - 1; i > tempArray.Length - 4; i--)
        {
            tempArray[i].Remove();
        }

        MenuEvilOption[] menuEvilOptions =
        {
            MenuEvilOption.Random, MenuEvilOption.Corruption, MenuEvilOption.Crimson, MenuEvilOption.Contagion
        };
        LocalizedText[] titles =
        {
            Lang.misc[103], Lang.misc[101], Lang.misc[102],
            Language.GetText("Mods.AvalonTesting.UI.WorldTitleEvilContagion")
        };
        LocalizedText[] descriptions =
        {
            Language.GetText("UI.WorldDescriptionEvilRandom"), Language.GetText("UI.WorldDescriptionEvilCorrupt"),
            Language.GetText("UI.WorldDescriptionEvilCrimson"),
            Language.GetText("Mods.AvalonTesting.UI.WorldDescriptionEvilContagion")
        };
        Color[] textColors = {Color.White, Color.MediumPurple, Color.IndianRed, Color.SpringGreen};
        Asset<Texture2D>[] array5 =
        {
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"),
            Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCrimson"),
            ModContent.Request<Texture2D>($"{AvalonTesting.AssetPath}Textures/UI/ContagionIcon")
        };

        EvilListGrid = new ExxoUIListGrid(2);
        EvilListGrid.Top.Set(accumulatedHeight, 0);
        EvilListGrid.Width.Set(0, 1);
        EvilListGrid.FitHeightToContent = true;
        EvilListGrid.ContentHAlign = 0.5f;
        EvilListGrid.ListPadding = 4;
        container.Append(EvilListGrid);

        for (int i = 0; i < menuEvilOptions.Length; i++)
        {
            ExxoUIGroupOptionButton<MenuEvilOption> groupOptionButton = new(menuEvilOptions[i], titles[i],
                descriptions[i], textColors[i], array5[i], 1f, 1f, 16f);

            groupOptionButton.OnMouseDown += (UIMouseEvent evt, UIElement element) =>
            {
                foreach (UIElement parent in EvilListGrid.Children)
                {
                    foreach (UIElement child in parent.Children)
                    {
                        (child as ExxoUIGroupOptionButton<MenuEvilOption>)!.SetCurrentOption(
                            (element as ExxoUIGroupOptionButton<MenuEvilOption>)!.OptionValue);
                    }
                }

                CurrentEvilOption = (element as ExxoUIGroupOptionButton<MenuEvilOption>)!.OptionValue;
                typeof(UIWorldCreation).GetMethod("UpdatePreviewPlate", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(self, null);
            };
            groupOptionButton.OnMouseOver += (evt, element) =>
            {
                GetDescriptionUIText(self).SetText((element as ExxoUIGroupOptionButton<MenuEvilOption>)!.Description);
            };
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;
            groupOptionButton.SetSnapPoint(tagGroup, i);
            groupOptionButton.SetCurrentOption(0);
            EvilListGrid.Append(groupOptionButton, new ExxoUIList.ElementParams(true, false));
        }

        EvilListGrid.Recalculate();

        container.Parent.Parent.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels - 36;
        container.Parent.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels - 36;
        container.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels - 36;
    }

    public static void ILFinishCreatingWorld(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchRet()))
        {
            return;
        }

        if (!c.TryGotoPrev(i => i.MatchLdnull()))
        {
            return;
        }

        c.EmitDelegate(() =>
        {
            switch (CurrentEvilOption)
            {
                case MenuEvilOption.Random:
                    WorldGen.WorldGenParam_Evil = -1;
                    break;
                case MenuEvilOption.Corruption:
                    WorldGen.WorldGenParam_Evil = 0;
                    break;
                case MenuEvilOption.Crimson:
                    WorldGen.WorldGenParam_Evil = 1;
                    break;
                case MenuEvilOption.Contagion:
                    WorldGen.WorldGenParam_Evil = 2;
                    break;
            }
        });
    }

    public static void ILUpdatePreviewPlate(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchConvU1()))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchConvU1()))
        {
            return;
        }

        c.Index++;
        c.Emit(OpCodes.Pop);

        c.EmitDelegate(() =>
        {
            byte option = (byte)CurrentEvilOption;
            if (option > 2)
            {
                option = 2;
            }

            return option;
        });
    }
}
