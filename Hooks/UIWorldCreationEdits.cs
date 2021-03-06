/*using System; commented just in case we reuse it somehow
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalon.Common;
using Avalon.Items.Placeable.Tile;
using Avalon.Systems;
using Avalon.UI;
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

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class UIWorldCreationEdits : ModHook
{
    private static readonly Func<UIWorldCreation, UIText> GetDescriptionUIText =
        Utilities.CreateInstancePropertyOrFieldReaderDelegate<UIWorldCreation, UIText>("_descriptionText");

    private static MenuEvilOption currentEvilOption;

    public enum MenuEvilOption
    {
        /// <summary>
        ///     Represents that no evil has been specified.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Represents that corruption has been selected.
        /// </summary>
        Corruption = 1,

        /// <summary>
        ///     Represents that crimson has been selected.
        /// </summary>
        Crimson = 2,

        /// <summary>
        ///     Represents that contagion has been selected.
        /// </summary>
        Contagion = 3,
    }

    protected override void Apply()
    {
        IL.Terraria.GameContent.UI.States.UIWorldCreation.MakeInfoMenu += ILMakeInfoMenu;
        On.Terraria.GameContent.UI.States.UIWorldCreation.AddWorldEvilOptions += OnAddWorldEvilOptions;
        IL.Terraria.GameContent.UI.States.UIWorldCreation.FinishCreatingWorld += ILFinishCreatingWorld;
        IL.Terraria.GameContent.UI.States.UIWorldCreation.UpdatePreviewPlate += ILUpdatePreviewPlate;
    }

    /// <summary>
    ///     Updates the preview plate with the new values from the UI replacement.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>
    private static void ILUpdatePreviewPlate(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdfld<UIWorldCreation>("_optionEvil"))
            .GotoNext(i => i.MatchConvU1());

        c.Index++;
        c.Emit(OpCodes.Pop);

        c.EmitDelegate(() =>
        {
            byte option = (byte)currentEvilOption;
            if (option > 2)
            {
                option = 2;
            }

            return option;
        });
    }

    /// <summary>
    ///     Skips the last separator and the space added as compensation.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>
    private static void ILMakeInfoMenu(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchLdstr("evil"))
            .GotoNext(i => i.MatchLdloc(1), i => i.MatchLdcR4(48f));

        ILLabel label = c.DefineLabel();
        c.Emit(OpCodes.Br, label)
            .GotoNext(i => i.MatchLdarg(0), i => i.MatchLdloc(0), i => i.MatchLdloc(1), i => i.MatchLdstr("desc"));

        c.MarkLabel(label);
    }

    private static void OnAddWorldEvilOptions(
        On.Terraria.GameContent.UI.States.UIWorldCreation.orig_AddWorldEvilOptions orig,
        UIWorldCreation self, UIElement container,
        float accumulatedHeight,
        UIElement.MouseEvent clickEvent,
        string tagGroup, float usableWidthPercent)
    {
        orig(self, container, accumulatedHeight, clickEvent, tagGroup, usableWidthPercent);

        // Remove original evil buttons
        float heightAdjustment = 0f;
        const string worldEvilIdName = "WorldEvilId";
        Type? worldEvilIdType = typeof(UIWorldCreation).GetNestedType(worldEvilIdName, BindingFlags.NonPublic);
        if (worldEvilIdType == null)
        {
            Avalon.Mod.Logger.Error(
                $"Could not find field with name {worldEvilIdName} in {typeof(UIWorldCreation)}");
        }
        else
        {
            Type groupOptionButtonType = typeof(GroupOptionButton<>).MakeGenericType(worldEvilIdType);
            foreach (UIElement? evilMenuOption in container.Children
                         .Where(child => child.GetType() == groupOptionButtonType).ToArray())
            {
                evilMenuOption.Remove();
                heightAdjustment = -evilMenuOption.GetOuterDimensions().Height;
            }
        }

        UIElement lastElement = container.Children.Last();
        accumulatedHeight = lastElement.Top.Pixels + lastElement.GetOuterDimensions().Height;

        var customOptionsPrimaryList = new ExxoUIList { ListPadding = 4, FitHeightToContent = true };
        customOptionsPrimaryList.Width.Set(0, 1);
        customOptionsPrimaryList.Top.Set(accumulatedHeight, 0);
        container.Append(customOptionsPrimaryList);

        AddCustomGenMenu(self, customOptionsPrimaryList, tagGroup,
            val =>
            {
                currentEvilOption = val ?? MenuEvilOption.None;
                typeof(UIWorldCreation).GetMethod(
                        "UpdatePreviewPlate",
                        BindingFlags.NonPublic | BindingFlags.Instance)!
                    .Invoke(self, null);
            }, MenuEvilOption.None, 2,
            new MenuEvilOption?[]
            {
                MenuEvilOption.None, MenuEvilOption.Corruption, MenuEvilOption.Crimson, MenuEvilOption.Contagion,
            },
            new[]
            {
                Lang.misc[103], Lang.misc[101], Lang.misc[102],
                Language.GetText("Mods.Avalon.UI.WorldTitleEvilContagion"),
            },
            new[]
            {
                Language.GetText("UI.WorldDescriptionEvilRandom"),
                Language.GetText("UI.WorldDescriptionEvilCorrupt"),
                Language.GetText("UI.WorldDescriptionEvilCrimson"),
                Language.GetText("Mods.Avalon.UI.WorldDescriptionEvilContagion"),
            }, new[] { Color.White, Color.MediumPurple, Color.IndianRed, Color.SpringGreen },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCorruption"),
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilCrimson"),
                Avalon.Mod.Assets.Request<Texture2D>(
                    $"{Avalon.TextureAssetsPath}/UI/ContagionIcon",
                    AssetRequestMode.ImmediateLoad),
            });

        // TODO: Wrapper fix for automatic height to max height
        var oreDrawersWrapper = new ExxoUIElementWrapper<ExxoUIList>(new ExxoUIList()) { OverflowHidden = true };
        oreDrawersWrapper.OverflowHidden = true;
        oreDrawersWrapper.MinHeight.Set(120, 0);
        oreDrawersWrapper.MaxHeight.Set(120, 0);
        oreDrawersWrapper.Width.Set(0, 1);
        ExxoUIList oreDrawers = oreDrawersWrapper.InnerElement;
        oreDrawers.Direction = Direction.Horizontal;
        oreDrawers.Width.Set(0, 1);
        oreDrawers.FitHeightToContent = true;
        oreDrawers.ListPadding = 32;
        customOptionsPrimaryList.Append(oreDrawersWrapper);

        var leftOreDrawer = new ExxoUIList { FitHeightToContent = true, ListPadding = 4 };
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

        AddCustomGenMenu(self, leftOreDrawer, "copperOres",
            val => ModContent.GetInstance<ExxoWorldGen>().CopperOre = val,
            null, 4,
            new ExxoWorldGen.CopperVariant?[]
            {
                null, ExxoWorldGen.CopperVariant.Copper, ExxoWorldGen.CopperVariant.Tin,
                ExxoWorldGen.CopperVariant.Bronze,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.CopperOre], TextureAssets.Item[ItemID.TinOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<BronzeOre>().Texture),
            });

        Main.instance.LoadItem(ItemID.IronOre);
        Main.instance.LoadItem(ItemID.LeadOre);

        AddCustomGenMenu(self, leftOreDrawer, "ironOres",
            val => ModContent.GetInstance<ExxoWorldGen>().IronOre = val,
            null, 4,
            new ExxoWorldGen.IronVariant?[]
            {
                null, ExxoWorldGen.IronVariant.Iron, ExxoWorldGen.IronVariant.Lead, ExxoWorldGen.IronVariant.Nickel,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.IronOre], TextureAssets.Item[ItemID.LeadOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<NickelOre>().Texture),
            });

        Main.instance.LoadItem(ItemID.SilverOre);
        Main.instance.LoadItem(ItemID.TungstenOre);

        AddCustomGenMenu(self, leftOreDrawer, "silverOres",
            val => ModContent.GetInstance<ExxoWorldGen>().SilverOre = val,
            null, 4,
            new ExxoWorldGen.SilverVariant?[]
            {
                null, ExxoWorldGen.SilverVariant.Silver, ExxoWorldGen.SilverVariant.Tungsten,
                ExxoWorldGen.SilverVariant.Zinc,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.SilverOre], TextureAssets.Item[ItemID.TungstenOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<ZincOre>().Texture),
            });

        Main.instance.LoadItem(ItemID.GoldOre);
        Main.instance.LoadItem(ItemID.PlatinumOre);

        AddCustomGenMenu(self, leftOreDrawer, "goldOres",
            val => ModContent.GetInstance<ExxoWorldGen>().GoldOre = val,
            null, 4,
            new ExxoWorldGen.GoldVariant?[]
            {
                null, ExxoWorldGen.GoldVariant.Gold, ExxoWorldGen.GoldVariant.Platinum,
                ExxoWorldGen.GoldVariant.Bismuth,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.GoldOre], TextureAssets.Item[ItemID.PlatinumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<BismuthOre>().Texture),
            });

        AddCustomGenMenu(self, leftOreDrawer, "rhodiumOres",
            val => ModContent.GetInstance<ExxoWorldGen>().RhodiumOre = val,
            null, 4,
            new ExxoWorldGen.RhodiumVariant?[]
            {
                null, ExxoWorldGen.RhodiumVariant.Rhodium, ExxoWorldGen.RhodiumVariant.Iridium,
                ExxoWorldGen.RhodiumVariant.Osmium,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                ModContent.Request<Texture2D>(ModContent.GetInstance<RhodiumOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<IridiumOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<OsmiumOre>().Texture),
            }, false);

        var rightOreDrawer = new ExxoUIList { FitHeightToContent = true, ListPadding = 4 };
        oreDrawers.Append(rightOreDrawer, new ExxoUIList.ElementParams(true, false));

        Main.instance.LoadItem(ItemID.CobaltOre);
        Main.instance.LoadItem(ItemID.PalladiumOre);

        AddCustomGenMenu(self, rightOreDrawer, "cobaltOres",
            val => ModContent.GetInstance<ExxoWorldGen>().CobaltOre = val,
            null, 4,
            new ExxoWorldGen.CobaltVariant?[]
            {
                null, ExxoWorldGen.CobaltVariant.Cobalt, ExxoWorldGen.CobaltVariant.Palladium,
                ExxoWorldGen.CobaltVariant.Duratanium,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.CobaltOre], TextureAssets.Item[ItemID.PalladiumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<DurataniumOre>().Texture),
            });

        Main.instance.LoadItem(ItemID.MythrilOre);
        Main.instance.LoadItem(ItemID.OrichalcumOre);

        AddCustomGenMenu(self, rightOreDrawer, "mythrilOres",
            val => ModContent.GetInstance<ExxoWorldGen>().MythrilOre = val,
            null, 4,
            new ExxoWorldGen.MythrilVariant?[]
            {
                null, ExxoWorldGen.MythrilVariant.Mythril, ExxoWorldGen.MythrilVariant.Orichalcum,
                ExxoWorldGen.MythrilVariant.Naquadah,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.MythrilOre], TextureAssets.Item[ItemID.OrichalcumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<NaquadahOre>().Texture),
            });

        Main.instance.LoadItem(ItemID.AdamantiteOre);
        Main.instance.LoadItem(ItemID.TitaniumOre);

        AddCustomGenMenu(self, rightOreDrawer, "adamantiteOres",
            val => ModContent.GetInstance<ExxoWorldGen>().AdamantiteOre = val,
            null, 4,
            new ExxoWorldGen.AdamantiteVariant?[]
            {
                null, ExxoWorldGen.AdamantiteVariant.Adamantite, ExxoWorldGen.AdamantiteVariant.Titanium,
                ExxoWorldGen.AdamantiteVariant.Troxinium,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                TextureAssets.Item[ItemID.AdamantiteOre], TextureAssets.Item[ItemID.TitaniumOre],
                ModContent.Request<Texture2D>(ModContent.GetInstance<TroxiniumOre>().Texture),
            });

        AddCustomGenMenu(self, rightOreDrawer, "shmTier1Ores",
            val => ModContent.GetInstance<ExxoWorldGen>().SHMTier1Ore = val,
            null, 4,
            new ExxoWorldGen.SHMTier1Variant?[]
            {
                null, ExxoWorldGen.SHMTier1Variant.Pyroscoric, ExxoWorldGen.SHMTier1Variant.Tritanorium,
            },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { LocalizedText.Empty, LocalizedText.Empty, LocalizedText.Empty },
            new[] { Color.White, Color.White, Color.White },
            new[]
            {
                Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom"),
                ModContent.Request<Texture2D>(ModContent.GetInstance<PyroscoricOre>().Texture),
                ModContent.Request<Texture2D>(ModContent.GetInstance<TritanoriumOre>().Texture),
            });

        AddCustomGenMenu(self, rightOreDrawer, "shmTier2Ores",
            val => ModContent.GetInstance<ExxoWorldGen>().SHMTier2Ore = val,
            null, 4,
            new ExxoWorldGen.SHMTier2Variant?[]
            {
                null, ExxoWorldGen.SHMTier2Variant.Unvolandite, ExxoWorldGen.SHMTier2Variant.Vorazylcum,
            },
            new[] { ItemID.None, ModContent.ItemType<UnvolanditeOre>(), ModContent.ItemType<VorazylcumOre>() }, false);

        var hr = new UIHorizontalSeparator();
        hr.Width.Set(0, 1);
        hr.Color = Color.Lerp(Color.White, new Color(63, 65, 151, byte.MaxValue), 0.85f) * 0.9f;
        customOptionsPrimaryList.Append(hr);

        customOptionsPrimaryList.Recalculate();

        heightAdjustment += customOptionsPrimaryList.MinHeight.Pixels;
        container.Parent.Parent.Parent.Height.Pixels += heightAdjustment;
        container.Parent.Parent.Height.Pixels += heightAdjustment;
        container.Parent.Height.Pixels += heightAdjustment;
    }

    private static void AddCustomGenMenu<T>(UIWorldCreation self, ExxoUIList container,
                                            string tagGroup, Action<T?> actionOnClick, T? defaultSelection,
                                            int amountPerInnerList, IReadOnlyList<T?> optionValues,
                                            IReadOnlyList<int> items, bool addHorizontalRule = true)
        where T : struct, IComparable
    {
        var titles = new LocalizedText[items.Count];
        var descriptions = new LocalizedText[items.Count];
        var textures = new Asset<Texture2D>[items.Count];
        var colors = new Color[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            titles[i] = LocalizedText.Empty;
            descriptions[i] = Lang.GetItemName(items[i]);
            textures[i] = TextureAssets.Item[items[i]];
            colors[i] = Color.White;
        }

        textures[0] = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconEvilRandom");
        descriptions[0] = Language.GetText("CLI.Random");

        AddCustomGenMenu(self, container, tagGroup, actionOnClick, defaultSelection, amountPerInnerList, optionValues,
            titles, descriptions, colors, textures, addHorizontalRule);
    }

    private static void AddCustomGenMenu<T>(UIWorldCreation self, ExxoUIList container,
                                            string tagGroup, Action<T?> actionOnClick, T? defaultSelection,
                                            int amountPerInnerList, IReadOnlyList<T?> optionValues,
                                            IReadOnlyList<LocalizedText> titles,
                                            IReadOnlyList<LocalizedText> descriptions, IReadOnlyList<Color> textColors,
                                            IReadOnlyList<Asset<Texture2D>> textures, bool addHorizontalRule = true)
        where T : struct, IComparable
    {
        var listGrid = new ExxoUIListGrid(amountPerInnerList);
        listGrid.Width.Set(0, 1);
        listGrid.FitHeightToContent = true;
        listGrid.ContentHAlign = 0.5f;
        listGrid.ListPadding = 4;
        container.Append(listGrid);

        for (int i = 0; i < optionValues.Count; i++)
        {
            ExxoUIGroupOptionButton<T> groupOptionButton = new(optionValues[i], titles[i],
                descriptions[i], textColors[i], textures[i], 1f, 1f, 16f);

            groupOptionButton.OnMouseDown += (_, element) =>
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
            groupOptionButton.OnMouseOver += (_, element) =>
                GetDescriptionUIText(self).SetText((element as ExxoUIGroupOptionButton<T>)!.Description);
            groupOptionButton.OnMouseOut += self.ClearOptionDescription;
            groupOptionButton.SetSnapPoint(tagGroup, i);
            groupOptionButton.SetCurrentOption(defaultSelection);
            listGrid.Append(groupOptionButton, new ExxoUIList.ElementParams(true, false));
        }

        if (addHorizontalRule)
        {
            var hr = new UIHorizontalSeparator();
            hr.Width.Set(0, 1);
            hr.Color = Color.Lerp(Color.White, new Color(63, 65, 151, byte.MaxValue), 0.85f) * 0.9f;
            container.Append(hr);
        }
    }

    /// <summary>
    ///     Sets the WorldGenParam_Evil corresponding to the final option.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>
    private static void ILFinishCreatingWorld(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(i => i.MatchRet())
            .GotoPrev(i => i.MatchLdnull());

        c.EmitDelegate(() =>
        {
            WorldGen.WorldGenParam_Evil = currentEvilOption switch
            {
                MenuEvilOption.None => -1,
                MenuEvilOption.Corruption => (int)ExxoWorldGen.EvilBiome.Corruption,
                MenuEvilOption.Crimson => (int)ExxoWorldGen.EvilBiome.Crimson,
                MenuEvilOption.Contagion => (int)ExxoWorldGen.EvilBiome.Contagion,
                _ => WorldGen.WorldGenParam_Evil,
            };
        });
    }
}*/
