using System.Collections.Generic;
using Avalon.Data;
using Avalon.Items.Potions;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI.Herbology;

public class HerbologyUIState : ExxoUIState
{
    private HerbologyUIHelpAttachment? helpAttachment;
    private ExxoUIImageButtonToggle? helpToggle;
    private HerbologyUIHerbCountAttachment? herbCountAttachment;
    private HerbologyUIHerbExchange? herbExchange;
    private ExxoUIDraggablePanel? mainPanel;
    private HerbologyUIPotionExchange? potionExchange;
    private HerbologyUIPurchaseAttachment? purchaseAttachment;
    private HerbologyUIStats? stats;
    private HerbologyUITurnIn? turnIn;

    public override void OnInitialize()
    {
        base.OnInitialize();

        ExxoHerbologyPlayer herbologyPlayer = Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>();

        helpAttachment = new HerbologyUIHelpAttachment();

        mainPanel = new ExxoUIDraggablePanel
        {
            Width = StyleDimension.FromPixels(720),
            Height = StyleDimension.FromPixels(660),
            VAlign = UIAlign.Center,
            HAlign = UIAlign.Center,
        };
        mainPanel.SetPadding(15);
        Append(mainPanel);

        var mainContainer = new ExxoUIList
        {
            Width = StyleDimension.Fill, Height = StyleDimension.Fill, ContentHAlign = UIAlign.Center,
        };
        mainPanel.Append(mainContainer);

        var titleRow = new ExxoUIList()
        {
            Width = StyleDimension.Fill,
            Direction = Direction.Horizontal,
            Justification = Justification.Center,
            FitHeightToContent = true,
            ContentVAlign = UIAlign.Center,
        };
        mainContainer.Append(titleRow);
        var titleText = new ExxoUITextPanel("Herbology Bench", 0.8f, true);
        titleRow.Append(titleText);

        helpToggle =
            new ExxoUIImageButtonToggle(Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", AssetRequestMode.ImmediateLoad),
                Color.White * 0.7f, Color.White) { Scale = 2, Tooltip = "Help" };
        titleRow.Append(helpToggle);
        helpToggle.OnToggle += (toggled) =>
        {
            helpAttachment.Enabled = toggled;
            helpToggle.MouseOver(new UIMouseEvent(helpToggle, UserInterface.ActiveInstance.MousePosition));
        };
        helpAttachment.Register(helpToggle,
            "When this button is active, hovering over elements provides a description of their purpose");

        var herbContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        herbContainer.Width.Set(0, 1);
        herbContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(herbContainer, new ExxoUIList.ElementParams(true, false));

        stats = new HerbologyUIStats();
        herbContainer.InnerElement.Append(stats);
        helpAttachment.Register(stats, "A list of herbology stats relating to the player");
        helpAttachment.Register(stats.RankTitleText, "Title of the current herbology tier");
        helpAttachment.Register(stats.HerbTierText, "Current herbology tier");
        helpAttachment.Register(stats.HerbTotalContainer, "Herb tokens used to purchase seeds in the herb exchange");
        helpAttachment.Register(stats.PotionTotalContainer,
            "Potion tokens used to purchase potions and elixirs in the potion exchange");

        turnIn = new HerbologyUITurnIn();
        herbContainer.InnerElement.Append(turnIn);
        helpAttachment.Register(turnIn, "Turn in herbs and potions in exchange for tokens");
        helpAttachment.Register(turnIn.ItemSlot, "Place items here to be exchanged for tokens");
        helpAttachment.Register(turnIn.Button, "Converts the current items in the item slot into tokens");

        turnIn.Button.OnClick += delegate
        {
            Item item = turnIn.ItemSlot.Item;

            ExxoHerbologyPlayer.HerbTier oldTier = Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier;
            if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().SellItem(item))
            {
                item.stack = 0;
                if (oldTier != Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier)
                {
                    RefreshContent();
                }
            }
        };

        herbExchange = new HerbologyUIHerbExchange();
        herbContainer.InnerElement.Append(herbExchange, new ExxoUIList.ElementParams(true, false));
        helpAttachment.Register(herbExchange, "Purchase herbs using herb tokens");
        helpAttachment.Register(herbExchange.Toggle, "Toggle listing between seeds and large seeds");
        helpAttachment.Register(herbExchange.Grid, "Select an item to purchase");

        herbExchange.Toggle.OnToggle += (toggled) => RefreshHerbList(toggled);
        herbExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment.AttachTo(null);
            herbCountAttachment.AttachTo(null);
        };

        Append(new ExxoUIContentLockPanel(herbExchange.Toggle,
            () => Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier >=
                  ExxoHerbologyPlayer.HerbTier.Apprentice,
            $"Content locked: Must be Herbology {ExxoHerbologyPlayer.HerbTier.Apprentice}"));

        var potionContainer = new ExxoUIPanelWrapper<ExxoUIList>(new ExxoUIList());
        potionContainer.Width.Set(0, 1);
        potionContainer.InnerElement.Direction = Direction.Horizontal;
        mainContainer.Append(potionContainer, new ExxoUIList.ElementParams(true, false));

        potionExchange = new HerbologyUIPotionExchange();
        potionContainer.InnerElement.Append(potionExchange, new ExxoUIList.ElementParams(true, false));
        helpAttachment.Register(potionExchange, "Purchase potions using potion tokens");
        helpAttachment.Register(potionExchange.Toggle, "Toggle listing between potions and elixirs");
        helpAttachment.Register(potionExchange.Grid, "Select an item to purchase");

        potionExchange.Toggle.OnToggle += (toggled) => RefreshPotionList(toggled);
        potionExchange.Scrollbar.OnViewPositionChanged += delegate
        {
            purchaseAttachment.AttachTo(null);
            herbCountAttachment.AttachTo(null);
        };

        var potionLock = new ExxoUIContentLockPanel(potionExchange,
            () => Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier >=
                  ExxoHerbologyPlayer.HerbTier.Expert,
            $"Content locked: Must be Herbology {ExxoHerbologyPlayer.HerbTier.Expert}");
        Append(potionLock);
        potionLock.OnLockStatusChanged += (sender, _) => potionExchange.Scrollbar.Active = !sender.Locked;

        purchaseAttachment = new HerbologyUIPurchaseAttachment();
        Append(purchaseAttachment);
        helpAttachment.Register(purchaseAttachment.NumberInputWithButtons,
            "Select amount of the selected item to purchase");
        helpAttachment.Register(purchaseAttachment.DifferenceContainer,
            "How the following purchase will affect your token balance");
        helpAttachment.Register(purchaseAttachment.Button, "Click to purchase items");

        purchaseAttachment.NumberInputWithButtons.NumberInput.OnKeyboardUpdate += (_, keyboardState) =>
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                purchaseAttachment.AttachTo(null);
                herbCountAttachment.AttachTo(null);
            }
            else if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (herbologyPlayer.PurchaseItem(purchaseAttachment.AttachmentHolder?.Item,
                        purchaseAttachment.NumberInputWithButtons.NumberInput.Number))
                {
                    purchaseAttachment.AttachTo(null);
                    herbCountAttachment.AttachTo(null);
                }
            }
        };

        purchaseAttachment.Button.OnClick += delegate
        {
            if (herbologyPlayer.PurchaseItem(purchaseAttachment.AttachmentHolder?.Item,
                    purchaseAttachment.NumberInputWithButtons.NumberInput.Number))
            {
                purchaseAttachment.AttachTo(null);
                herbCountAttachment.AttachTo(null);
            }
        };

        herbCountAttachment = new HerbologyUIHerbCountAttachment();
        Append(herbCountAttachment);
        helpAttachment.Register(herbCountAttachment.AttachmentElement,
            "The amount of herbs of that type available, needed to purchase large herb seeds");

        Append(helpAttachment);

        RefreshContent();
    }

    public override void RightDoubleClick(UIMouseEvent evt)
    {
        base.RightDoubleClick(evt);
        // if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier ==
        //     ExxoHerbologyPlayer.HerbTier.Master)
        // {
        //     Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier =
        //         ExxoHerbologyPlayer.HerbTier.Novice;
        // }
        // else
        // {
        //     Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier++;
        // }
        //
        // RefreshContent();
    }

    public override void OnActivate()
    {
        base.OnActivate();
        Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().UpdateHerbTier();
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        SoundEngine.PlaySound(SoundID.MenuClose);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Player player = Main.LocalPlayer;
        ExxoHerbologyPlayer modPlayer = player.GetModPlayer<ExxoHerbologyPlayer>();

        if (player.chest != -1 || Main.npcShop != 0)
        {
            modPlayer.DisplayHerbologyMenu = false;
            player.dropItemCheck();
            Recipe.FindRecipes();
        }
    }

    public override void Click(UIMouseEvent evt)
    {
        base.Click(evt);
        if (!purchaseAttachment.ContainsPoint(evt.MousePosition) &&
            !herbCountAttachment.ContainsPoint(evt.MousePosition) &&
            purchaseAttachment.AttachmentHolder?.ContainsPoint(evt.MousePosition) == false)
        {
            purchaseAttachment.AttachTo(null);
            herbCountAttachment.AttachTo(null);
        }
    }

    private void RefreshContent()
    {
        RefreshHerbList(herbExchange.Toggle.Toggled);
        RefreshPotionList(potionExchange.Toggle.Toggled);
    }

    private void RefreshHerbList(bool displayLargeSeed)
    {
        herbExchange.Grid.InnerElement.Clear();
        var items = new List<int>();
        if (displayLargeSeed)
        {
            items.AddRange(HerbologyData.LargeHerbSeedIdByHerbSeedId.Values);
        }
        else
        {
            items.AddRange(HerbologyData.LargeHerbSeedIdByHerbSeedId.Keys);
        }

        herbExchange.Grid.InnerElement.RemoveAllChildren();
        herbExchange.Grid.InnerElement.Clear();

        var elements = new List<UIElement>();
        foreach (int itemID in items)
        {
            var herbItem = new ExxoUIItemSlot(TextureAssets.InventoryBack7, itemID);
            herbItem.OnClick += (UIMouseEvent _, UIElement listeningElement) =>
            {
                purchaseAttachment.AttachTo(listeningElement as ExxoUIItemSlot);
                herbCountAttachment.AttachTo(listeningElement as ExxoUIItemSlot);
                purchaseAttachment.NumberInputWithButtons.NumberInput.MaxNumber = herbItem.Item.maxStack;
            };
            herbExchange.Grid.InnerElement.Append(herbItem);
        }

        //herbExchange.Grid.InnerElement.AddRange(elements);
    }

    private void RefreshPotionList(bool displayElixirs)
    {
        potionExchange.Grid.InnerElement.Clear();
        var items = new List<int>();
        if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier >=
            ExxoHerbologyPlayer.HerbTier.Master)
        {
            items.Add(ItemID.SuperHealingPotion);
            items.Add(ItemID.SuperManaPotion);
            items.Add(ModContent.ItemType<SuperStaminaPotion>());
        }

        if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier >=
            ExxoHerbologyPlayer.HerbTier.Expert)
        {
            items.Add(ItemID.HealingPotion);
            items.Add(ItemID.ManaPotion);
            items.Add(ModContent.ItemType<StaminaPotion>());
        }

        if (displayElixirs)
        {
            items.AddRange(HerbologyData.ElixirIds);
        }
        else
        {
            items.AddRange(HerbologyData.PotionIds);
        }

        if (Main.LocalPlayer.GetModPlayer<ExxoHerbologyPlayer>().Tier >=
            ExxoHerbologyPlayer.HerbTier.Master)
        {
            items.Add(ModContent.ItemType<BlahPotion>());
        }

        potionExchange.Grid.InnerElement.RemoveAllChildren();
        potionExchange.Grid.InnerElement.Clear();

        foreach (int itemID in items)
        {
            var potionItem = new ExxoUIItemSlot(TextureAssets.InventoryBack7, itemID);
            potionItem.OnClick += (UIMouseEvent _, UIElement listeningElement) =>
            {
                purchaseAttachment.AttachTo(listeningElement as ExxoUIItemSlot);
                herbCountAttachment.AttachTo(null);
                purchaseAttachment.NumberInputWithButtons.NumberInput.MaxNumber = potionItem.Item.maxStack;
            };
            if (itemID == ModContent.ItemType<BlahPotion>())
            {
                potionItem.SetImage(TextureAssets.InventoryBack7);
            }

            potionExchange.Grid.InnerElement.Append(potionItem);
        }

        //potionExchange.Grid.InnerElement.AddRange(elements);
    }
}
