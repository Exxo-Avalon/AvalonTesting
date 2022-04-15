using System;
using System.Linq;
using AvalonTesting.UI;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.UI;

namespace AvalonTesting.Hooks;

public static class UIWorldCreationEdits
{
    public static Func<UIWorldCreation, UIText> GetDescriptionUIText =
        Utilities.CreatePropertyOrFieldReaderDelegate<UIWorldCreation, UIText>("_descriptionText");

    public static ExxoUIListGrid EvilListGrid;

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
        c.EmitDelegate(() => EvilListGrid.MinHeight.Pixels + 16);
    }

    public static void OnAddWorldEvilOptions(
        On.Terraria.GameContent.UI.States.UIWorldCreation.orig_AddWorldEvilOptions orig,
        UIWorldCreation self, UIElement container,
        float accumualtedHeight,
        UIElement.MouseEvent clickEvent,
        string tagGroup, float usableWidthPercent)
    {
        orig(self, container, accumualtedHeight, clickEvent, tagGroup, usableWidthPercent);
        UIElement[] tempArray = container.Children.ToArray();
        for (int i = tempArray.Length - 1; i > tempArray.Length - 4; i--)
        {
            tempArray[i].Remove();
        }

        int[] array = {0, 1, 2, 3};
        LocalizedText[] array2 =
        {
            Lang.misc[103], Lang.misc[101], Lang.misc[102],
            Language.GetText("Mods.AvalonTesting.UI.WorldTitleEvilContagion")
        };
        LocalizedText[] array3 =
        {
            Language.GetText("UI.WorldDescriptionEvilRandom"), Language.GetText("UI.WorldDescriptionEvilCorrupt"),
            Language.GetText("UI.WorldDescriptionEvilCrimson"),
            Language.GetText("Mods.AvalonTesting.UI.WorldDescriptionEvilContagion")
        };
        Color[] array4 = {Color.White, Color.MediumPurple, Color.IndianRed, Color.ForestGreen};
        string[] array5 =
        {
            "Images/UI/WorldCreation/IconEvilRandom", "Images/UI/WorldCreation/IconEvilCorruption",
            "Images/UI/WorldCreation/IconEvilCrimson", "Images/UI/WorldCreation/IconEvilRandom"
        };

        EvilListGrid = new ExxoUIListGrid();
        EvilListGrid.Top.Set(accumualtedHeight, 0);
        EvilListGrid.Width.Set(0, 1);
        EvilListGrid.FitHeightToContent = true;
        container.Append(EvilListGrid);

        for (int i = 0; i < array.Length; i++)
        {
            GroupOptionButton<int> groupOptionButton =
                new(array[i], array2[i], array3[i], array4[i], array5[i], 1f, 1f, 16f);
            groupOptionButton.Width = StyleDimension.FromPixelsAndPercent(-4 * 2, 1f / 3 * usableWidthPercent);
            // groupOptionButton.Left = StyleDimension.FromPercent(1f - usableWidthPercent);
            //groupOptionButton.HAlign = i / (float)2;
            //groupOptionButton.Top.Set(accumualtedHeight, 0f);
            groupOptionButton.OnMouseDown += (UIMouseEvent evt, UIElement element) =>
            {
                foreach (UIElement child in EvilListGrid.Children)
                {
                    (child as GroupOptionButton<int>).SetCurrentOption((element as GroupOptionButton<int>).OptionValue);
                }
            };
            groupOptionButton.OnMouseOver += (evt, element) =>
            {
                GetDescriptionUIText(self).SetText((element as GroupOptionButton<int>).Description);
            };
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;
            groupOptionButton.SetSnapPoint(tagGroup, i);
            groupOptionButton.SetCurrentOption(0);
            EvilListGrid.Append(groupOptionButton);
        }
        
        container.Parent.Parent.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels + 40;
        container.Parent.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels + 40;
        container.Parent.Height.Pixels += EvilListGrid.MinHeight.Pixels + 40;
        
        EvilListGrid.Recalculate();
    }
}
