using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Common;

public abstract class ModChest : ModTile
{
    protected virtual int ChestKeyItemId => ItemID.None;
    protected virtual short TileOreFinderPriority => 500;
    protected abstract int ChestItemId { get; }
    protected abstract bool CanBeLocked { get; }
    protected virtual Color UnlockedMapColor => Color.Gray;
    protected virtual Color LockedMapColor => Color.DarkGray;

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ChestDrop);
        Chest.DestroyChest(i, j);
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        int left = i;
        int top = j;
        if (tile.TileFrameX % 36 != 0)
        {
            left--;
        }

        if (tile.TileFrameY != 0)
        {
            top--;
        }

        int chest = Chest.FindChest(left, top);
        if (chest < 0)
        {
            player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
        }
        else
        {
            player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Example Chest";
            if (player.cursorItemIconText == "Example Chest")
            {
                player.cursorItemIconID = ChestItemId;
                if (Main.tile[left, top].TileFrameX / 36 == 1)
                {
                    player.cursorItemIconID = ChestKeyItemId;
                }

                player.cursorItemIconText = string.Empty;
            }
        }

        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
    }

    public override void MouseOverFar(int i, int j)
    {
        MouseOver(i, j);
        Player player = Main.LocalPlayer;
        if (player.cursorItemIconText?.Length == 0)
        {
            player.cursorItemIconEnabled = false;
            player.cursorItemIconID = 0;
        }
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = 1;

    public override bool RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        Main.mouseRightRelease = false;
        int left = i;
        int top = j;
        if (tile.TileFrameX % 36 != 0)
        {
            left--;
        }

        if (tile.TileFrameY != 0)
        {
            top--;
        }

        if (player.sign >= 0)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            player.sign = -1;
            Main.editSign = false;
            Main.npcChatText = string.Empty;
        }

        if (Main.editChest)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            Main.editChest = false;
            Main.npcChatText = string.Empty;
        }

        if (player.editedChestName)
        {
            NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1,
                NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
            player.editedChestName = false;
        }

        bool isLocked = IsLockedChest(left, top);
        if (Main.netMode == NetmodeID.MultiplayerClient && !isLocked)
        {
            if (left == player.chestX && top == player.chestY && player.chest >= 0)
            {
                player.chest = -1;
                Recipe.FindRecipes();
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
            else
            {
                NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);
                Main.stackSplit = 600;
            }
        }
        else if (isLocked)
        {
            if (player.ConsumeItem(ChestKeyItemId) && Chest.Unlock(left, top) &&
                Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.Unlock, -1, -1, null, player.whoAmI, 1f, left, top);
            }
        }
        else
        {
            int chest = Chest.FindChest(left, top);
            if (chest >= 0)
            {
                Main.stackSplit = 600;
                if (chest == player.chest)
                {
                    player.chest = -1;
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
                else if (chest != player.chest && player.chest == -1)
                {
                    player.OpenChest(left, top, chest);
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                }
                else
                {
                    player.OpenChest(left, top, chest);
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }

                Recipe.FindRecipes();
            }
        }

        return true;
    }

    public override void SetStaticDefaults()
    {
        // Properties
        Main.tileSpelunker[Type] = true;
        Main.tileContainer[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileOreFinderPriority[Type] = TileOreFinderPriority;
        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.BasicChest[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;

        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
        TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
        TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
        TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.AnchorBottom = new AnchorData(
            AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);

        AdjTiles = new int[] { TileID.Containers };
        ChestDrop = ChestItemId;

        ModTranslation name = CreateMapEntryName();
        name.SetDefault(ContainerName.GetDefault());
        AddMapEntry(UnlockedMapColor, name, MapChestName);

        if (CanBeLocked)
        {
            name = CreateMapEntryName($"{Name}_Locked"); // With multiple map entries, you need unique translation keys.
            name.SetDefault($"Locked {ContainerName.GetDefault()}");
            AddMapEntry(LockedMapColor, name, MapChestName);
        }
    }

    protected virtual string MapChestName(string name, int i, int j)
    {
        int left = i;
        int top = j;
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX % 36 != 0)
        {
            left--;
        }

        if (tile.TileFrameY != 0)
        {
            top--;
        }

        int chest = Chest.FindChest(left, top);
        if (chest < 0)
        {
            return Language.GetTextValue("LegacyChestType.0");
        }

        if (Main.chest[chest].name?.Length == 0)
        {
            return name;
        }

        return $"{name}: {Main.chest[chest].name}";
    }
}
