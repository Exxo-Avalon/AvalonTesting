using System;
using System.Linq;
using System.Reflection;
using AvalonTesting.Items.Placeable.Tile;
using AvalonTesting.Systems;
using AvalonTesting.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
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
        Utilities.CreateInstancePropertyOrFieldReaderDelegate<UIWorldCreation, UIText>("_descriptionText");

    public static Action<UIElement, float> AddHorizontalSeperator =
        (Action<UIElement, float>)Delegate.CreateDelegate(typeof(Action<UIElement, float>),
            typeof(UIWorldCreation).GetMethod("AddHorizontalSeparator", BindingFlags.Static | BindingFlags.NonPublic));

    private static MenuEvilOption CurrentEvilOption;
    private static float MenuOffset;

    public static void ILMakeInfoMenu(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(i => i.MatchLdstr("evil")))
        {
            return;
        }

        if (!c.TryGotoNext(i => i.MatchLdloc(1)))
        {
            return;
        }

        Utilities.RemoveUntilInstruction(c, i => i.MatchLdarg(0));
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

        #region populating region

        var customOptionsPrimaryList = new ExxoUIList();
        customOptionsPrimaryList.ListPadding = 4;
        customOptionsPrimaryList.FitHeightToContent = true;
        customOptionsPrimaryList.Width.Set(0, 1);
        customOptionsPrimaryList.Top.Set(accumulatedHeight, 0);
        container.Append(customOptionsPrimaryList);

        AddCustomGenMenu<MenuEvilOption>(self, customOptionsPrimaryList, tagGroup,
            val =>
            {
                CurrentEvilOption = val;
                typeof(UIWorldCreation).GetMethod("UpdatePreviewPlate",
                        BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(self, null);
            }, MenuEvilOption.Random, 2,
            new[] {MenuEvilOption.Random, MenuEvilOption.Corruption, MenuEvilOption.Crimson, MenuEvilOption.Contagion},
            new[]
            {
                Lang.misc[103], Lang.misc[101], Lang.misc[102],
                Language.GetText("Mods.AvalonTesting.UI.WorldTitleEvilContagion")
            },
            new[]
            {
                Language.GetText("UI.WorldDescriptionEvilRandom"),
                Language.GetText("UI.WorldDescriptionEvilCorrupt"),
                Language.GetText("UI.WorldDescriptionEvilCrimson"),
                Language.GetText("Mods.AvalonTesting.UI.WorldDescriptionEvilContagion")
            }, new[] {Color.White, Color.MediumPurple, Color.IndianRed, Color.SpringGreen},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"),
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCrimson"),
                AvalonTesting.Mod.Assets.Request<Texture2D>($"{AvalonTesting.TextureAssetsPath}/UI/ContagionIcon")
            });

        // TODO: Wrapper fix for automatic height to max height
        var oreDrawersWrapper = new ExxoUIElementWrapper<ExxoUIList>(new ExxoUIList());
        oreDrawersWrapper.OverflowHidden = true;
        oreDrawersWrapper.MinHeight.Set(120, 0);
        oreDrawersWrapper.MaxHeight.Set(120, 0);
        oreDrawersWrapper.Width.Set(0, 1);
        var oreDrawers = oreDrawersWrapper.InnerElement;
        oreDrawers.Direction = Direction.Horizontal;
        oreDrawers.Width.Set(0, 1);
        oreDrawers.FitHeightToContent = true;
        oreDrawers.ListPadding = 32;
        customOptionsPrimaryList.Append(oreDrawersWrapper);

        var leftOreDrawer = new ExxoUIList();
        leftOreDrawer.FitHeightToContent = true;
        leftOreDrawer.ListPadding = 4;
        oreDrawers.Append(leftOreDrawer, new ExxoUIList.ElementParams(true, false));

        var oreDrawerScrollBar = new ExxoUIScrollbar();
        oreDrawerScrollBar.Height.Set(0, 0.8f);
        oreDrawerScrollBar.VAlign = 0.5f;
        oreDrawerScrollBar.HAlign = 0.5f;
        oreDrawersWrapper.Append(oreDrawerScrollBar);

        oreDrawers.ScrollBar = oreDrawerScrollBar;
        oreDrawersWrapper.OnScrollWheel += oreDrawers.ScrollWheelListener;

        Main.instance.LoadItem(ItemID.CopperOre);
        Main.instance.LoadItem(ItemID.TinOre);

        AddCustomGenMenu<ExxoWorldGen.CopperVariant>(self, leftOreDrawer, "copperOres",
            val => ModContent.GetInstance<ExxoWorldGen>().CopperOre = val,
            ExxoWorldGen.CopperVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.CopperVariant.Random, ExxoWorldGen.CopperVariant.Copper,
                ExxoWorldGen.CopperVariant.Tin, ExxoWorldGen.CopperVariant.Bronze
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.CopperOre], TextureAssets.Item[ItemID.TinOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<BronzeOre>().Texture)
            });

        Main.instance.LoadItem(ItemID.IronOre);
        Main.instance.LoadItem(ItemID.LeadOre);

        AddCustomGenMenu<ExxoWorldGen.IronVariant>(self, leftOreDrawer, "ironOres",
            val => ModContent.GetInstance<ExxoWorldGen>().IronOre = val,
            ExxoWorldGen.IronVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.IronVariant.Random, ExxoWorldGen.IronVariant.Iron, ExxoWorldGen.IronVariant.Lead,
                ExxoWorldGen.IronVariant.Nickel
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.IronOre], TextureAssets.Item[ItemID.LeadOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<NickelOre>().Texture)
            });

        Main.instance.LoadItem(ItemID.SilverOre);
        Main.instance.LoadItem(ItemID.TungstenOre);

        AddCustomGenMenu<ExxoWorldGen.SilverVariant>(self, leftOreDrawer, "silverOres",
            val => ModContent.GetInstance<ExxoWorldGen>().SilverOre = val,
            ExxoWorldGen.SilverVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.SilverVariant.Random, ExxoWorldGen.SilverVariant.Silver,
                ExxoWorldGen.SilverVariant.Tungsten, ExxoWorldGen.SilverVariant.Zinc
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.SilverOre], TextureAssets.Item[ItemID.TungstenOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<ZincOre>().Texture)
            });

        Main.instance.LoadItem(ItemID.GoldOre);
        Main.instance.LoadItem(ItemID.PlatinumOre);

        AddCustomGenMenu<ExxoWorldGen.GoldVariant>(self, leftOreDrawer, "goldOres",
            val => ModContent.GetInstance<ExxoWorldGen>().GoldOre = val,
            ExxoWorldGen.GoldVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.GoldVariant.Random, ExxoWorldGen.GoldVariant.Gold, ExxoWorldGen.GoldVariant.Platinum,
                ExxoWorldGen.GoldVariant.Bismuth
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.GoldOre], TextureAssets.Item[ItemID.PlatinumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<BismuthOre>().Texture)
            });

        AddCustomGenMenu<ExxoWorldGen.RhodiumVariant>(self, leftOreDrawer, "rhodiumOres",
            val => ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = val,
            ExxoWorldGen.RhodiumVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.RhodiumVariant.Random, ExxoWorldGen.RhodiumVariant.Rhodium,
                ExxoWorldGen.RhodiumVariant.Iridium, ExxoWorldGen.RhodiumVariant.Osmium
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                ModContent.Request<Texture2D>(ModContent.GetInstance<RhodiumOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<IridiumOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<OsmiumOre>().Texture)
            }, false);

        var rightOreDrawer = new ExxoUIList();
        rightOreDrawer.FitHeightToContent = true;
        rightOreDrawer.ListPadding = 4;
        oreDrawers.Append(rightOreDrawer, new ExxoUIList.ElementParams(true, false));

        Main.instance.LoadItem(ItemID.CobaltOre);
        Main.instance.LoadItem(ItemID.PalladiumOre);

        AddCustomGenMenu<ExxoWorldGen.CobaltVariant>(self, rightOreDrawer, "cobaltOres",
            val => ModContent.GetInstance<ExxoWorldGen>().CobaltOre = val,
            ExxoWorldGen.CobaltVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.CobaltVariant.Random, ExxoWorldGen.CobaltVariant.Cobalt,
                ExxoWorldGen.CobaltVariant.Palladium, ExxoWorldGen.CobaltVariant.Duratanium
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.CobaltOre], TextureAssets.Item[ItemID.PalladiumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<DurataniumOre>().Texture)
            });

        Main.instance.LoadItem(ItemID.MythrilOre);
        Main.instance.LoadItem(ItemID.OrichalcumOre);

        AddCustomGenMenu<ExxoWorldGen.MythrilVariant>(self, rightOreDrawer, "mythrilOres",
            val => ModContent.GetInstance<ExxoWorldGen>().MythrilOre = val,
            ExxoWorldGen.MythrilVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.MythrilVariant.Random, ExxoWorldGen.MythrilVariant.Mythril,
                ExxoWorldGen.MythrilVariant.Orichalcum, ExxoWorldGen.MythrilVariant.Naquadah
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.MythrilOre], TextureAssets.Item[ItemID.OrichalcumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<NaquadahOre>().Texture)
            });

        Main.instance.LoadItem(ItemID.AdamantiteOre);
        Main.instance.LoadItem(ItemID.TitaniumOre);

        AddCustomGenMenu<ExxoWorldGen.AdamantiteVariant>(self, rightOreDrawer, "adamantiteOres",
            val => ModContent.GetInstance<ExxoWorldGen>().AdamantiteOre = val,
            ExxoWorldGen.AdamantiteVariant.Random, 4,
            new[]
            {
                ExxoWorldGen.AdamantiteVariant.Random, ExxoWorldGen.AdamantiteVariant.Adamantite,
                ExxoWorldGen.AdamantiteVariant.Titanium, ExxoWorldGen.AdamantiteVariant.Troxinium
            },
            new LocalizedText[] {null, null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.AdamantiteOre], TextureAssets.Item[ItemID.TitaniumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<TroxiniumOre>().Texture)
            });

        AddCustomGenMenu<ExxoWorldGen.SHMTier1Variant>(self, rightOreDrawer, "shmTier1Ores",
            val => ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore = val,
            ExxoWorldGen.SHMTier1Variant.Random, 4,
            new[]
            {
                ExxoWorldGen.SHMTier1Variant.Random, ExxoWorldGen.SHMTier1Variant.Pyroscoric,
                ExxoWorldGen.SHMTier1Variant.Tritanorium
            },
            new LocalizedText[] {null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                ModContent.Request<Texture2D>(ModContent.GetInstance<PyroscoricOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<TritanoriumOre>().Texture)
            });

        AddCustomGenMenu<ExxoWorldGen.SHMTier2Variant>(self, rightOreDrawer, "shmTier2Ores",
            val => ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore = val,
            ExxoWorldGen.SHMTier2Variant.Random, 4,
            new[]
            {
                ExxoWorldGen.SHMTier2Variant.Random, ExxoWorldGen.SHMTier2Variant.Unvolandite,
                ExxoWorldGen.SHMTier2Variant.Vorazylcum
            },
            new LocalizedText[] {null, null, null},
            new[] {LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty},
            new[] {Color.White, Color.White, Color.White},
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                ModContent.Request<Texture2D>(ModContent.GetInstance<UnvolanditeOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<VorazylcumOre>().Texture)
            }, false);

        // float a = accumulatedHeight;
        // AddCustomGenMenu<MenuEvilOption>(self, container, ref a, tagGroup,
        //     val =>
        //     {
        //         CurrentEvilOption = val;
        //         typeof(UIWorldCreation).GetMethod("UpdatePreviewPlate",
        //                 BindingFlags.Instance | BindingFlags.NonPublic)
        //             .Invoke(self, null);
        //     }, MenuEvilOption.Random, 4, menuEvilOptions, titles, descriptions, textColors, array5);

        #endregion

        //  hr line in thing and ref implemented

        var hr = new UIHorizontalSeparator();
        hr.Width.Set(0, 1);
        hr.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int)byte.MaxValue), 0.85f) * 0.9f;
        customOptionsPrimaryList.Append(hr);

        customOptionsPrimaryList.Recalculate();

        container.Parent.Parent.Parent.Height.Pixels += customOptionsPrimaryList.MinHeight.Pixels - 48;
        container.Parent.Parent.Height.Pixels += customOptionsPrimaryList.MinHeight.Pixels - 48;
        container.Parent.Height.Pixels += customOptionsPrimaryList.MinHeight.Pixels - 48;
    }

    public static void AddCustomGenMenu<T>(UIWorldCreation self, ExxoUIList container,
                                           string tagGroup, Action<T> actionOnClick, T defaultSelection,
                                           int amountPerInnerList, T[] optionValues, LocalizedText[] titles,
                                           LocalizedText[] descriptions, Color[] textColors,
                                           Asset<Texture2D>[] textures, bool addHorizontalRule = true) where T : IComparable
    {
        var listGrid = new ExxoUIListGrid(amountPerInnerList);
        listGrid.Width.Set(0, 1);
        listGrid.FitHeightToContent = true;
        listGrid.ContentHAlign = 0.5f;
        listGrid.ListPadding = 4;
        container.Append(listGrid);

        for (int i = 0; i < optionValues.Length; i++)
        {
            ExxoUIGroupOptionButton<T> groupOptionButton = new(optionValues[i], titles[i],
                descriptions[i], textColors[i], textures[i], 1f, 1f, 16f);

            groupOptionButton.OnMouseDown += (UIMouseEvent evt, UIElement element) =>
            {
                foreach (UIElement parent in listGrid.Children)
                {
                    foreach (UIElement child in parent.Children)
                    {
                        (child as ExxoUIGroupOptionButton<T>)!.SetCurrentOption(
                            (element as ExxoUIGroupOptionButton<T>)!.OptionValue);
                    }
                }

                actionOnClick.Invoke((element as ExxoUIGroupOptionButton<T>)!.OptionValue);
            };
            groupOptionButton.OnMouseOver += (evt, element) =>
            {
                GetDescriptionUIText(self).SetText((element as ExxoUIGroupOptionButton<T>)!.Description);
            };
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;
            groupOptionButton.SetSnapPoint(tagGroup, i);
            groupOptionButton.SetCurrentOption(defaultSelection);
            listGrid.Append(groupOptionButton, new ExxoUIList.ElementParams(true, false));
        }

        if (addHorizontalRule)
        {
            var hr = new UIHorizontalSeparator();
            hr.Width.Set(0, 1);
            hr.Color = Color.Lerp(Color.White, new Color(63, 65, 151, (int)byte.MaxValue), 0.85f) * 0.9f;
            container.Append(hr);
        }
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
