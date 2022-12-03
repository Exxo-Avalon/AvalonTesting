using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;

namespace Avalon.Tiles;

//UNFINISHED. UNLOCKS WITH CONTAGION KEY, CHANGE WHEN TROPICS KEY IS ADDED
public class LockedTropicsChest : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSpelunker[Type] = true;
        Main.tileContainer[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1200;
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileOreFinderPriority[Type] = 500;
        TileID.Sets.HasOutlines[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.Origin = new Point16(0, 1);
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
        TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook((Chest.FindEmptyChest), -1, 0, true);
        TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook((Chest.AfterPlacement_Hook), -1, 0, false);
        TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
        var name = CreateMapEntryName();
        name.SetDefault("Locked Tropics Chest");
        AddMapEntry(new Color(200, 20, 0), name, MapChestName);
        TileID.Sets.DisableSmartCursor[Type] = true;
        AdjTiles = new int[] { TileID.Containers };
        ContainerName.SetDefault("Locked Tropics Chest");
        ChestDrop = ModContent.ItemType<Items.Placeable.Storage.ContagionChest>();
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public string MapChestName(string name, int i, int j)
    {
        var left = i;
        var top = j;
        var tile = Main.tile[i, j];
        if (tile.TileFrameX % 36 != 0)
        {
            left--;
        }
        if (tile.TileFrameY != 0)
        {
            top--;
        }
        var chest = Chest.FindChest(left, top);
        if (Main.chest[chest].name == "")
        {
            return name;
        }
        else
        {
            return name + ": " + Main.chest[chest].name;
        }
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = 6;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ChestDrop);
        Chest.DestroyChest(i, j);
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        blockDamaged = false;
        return false;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public static void Unlock(int X, int Y)
    {
        SoundEngine.PlaySound(SoundID.Unlock, new Vector2(X * 16, Y * 16));
        for (int i = X; i <= X + 1; i++)
        {
            for (int j = Y; j <= Y + 1; j++)
            {
                Main.tile[i, j].TileType = (ushort)ModContent.TileType<TropicsChest>();
                for (int k = 0; k < 4; k++)
                {
                    Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.Torch, 0f, 0f, 0, default, 1f);
                }
            }
        }
    }
    public override bool RightClick(int i, int j)
    {
        int num148;
        for (num148 = (int)(Main.tile[i, j].TileFrameX / 18); num148 > 1; num148 -= 2)
        {
        }
        num148 = i - num148;
        int num149 = j - (int)(Main.tile[i, j].TileFrameY / 18);
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        Main.mouseRightRelease = false;
        for (int num146 = 0; num146 < player.inventory.Length; num146++)
        {
            if (player.inventory[num146].type == ModContent.ItemType<Items.Other.ContagionKey>() && player.inventory[num146].stack > 0)
            {
                player.inventory[num146].stack--;
                if (player.inventory[num146].stack <= 0)
                {
                    player.inventory[num146] = new Item();
                }
                Unlock(num148, num149);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.Unlock, -1, -1, NetworkText.Empty, player.whoAmI, 1f, (float)num148, (float)num149, 0);
                }
            }
        }
        return true;
    }

    public override void MouseOver(int i, int j)
    {
        var player = Main.LocalPlayer;
        var tile = Main.tile[i, j]; 
        var left = i;
        var top = j;
        if (tile.TileFrameX % 36 != 0)
        {
            left--;
        }
        if (tile.TileFrameY != 0)
        {
            top--;
        }
        var chest = Chest.FindChest(left, top);
        player.cursorItemIconID = -1;
        if (chest < 0)
        {
            player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
        }
        else
        {
            player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Locked Tropics Chest";
            if (player.cursorItemIconText == "Locked Tropics Chest")
            {
                player.cursorItemIconID = ModContent.ItemType<Items.Other.ContagionKey>();
                player.cursorItemIconText = "";
            }
        }
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
    }

    public override void MouseOverFar(int i, int j)
    {
        MouseOver(i, j);
        var player = Main.LocalPlayer;
        if (player.cursorItemIconText == "")
        {
            player.cursorItemIconEnabled = false;
            player.cursorItemIconID = 0;
        }
    }
}
