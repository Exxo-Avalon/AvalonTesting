using AvalonTesting.Items.Placeable.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Tiles;

public class CoughwoodDoorOpen : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileLavaDeath[Type] = true;
        Main.tileNoSunLight[Type] = true;
        TileObjectData.newTile.Width = 2;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.Origin = new Point16(0, 0);
        TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 0);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
        TileObjectData.newTile.UsesCustomCanPlace = true;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.StyleMultiplier = 2;
        TileObjectData.newTile.StyleWrapLimit = 2;
        TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 1);
        TileObjectData.addAlternate(0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(0, 2);
        TileObjectData.addAlternate(0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 0);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 1);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
        TileObjectData.newAlternate.Origin = new Point16(1, 2);
        TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
        TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
        TileObjectData.addAlternate(1);
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        TileID.Sets.HousingWalls[Type] = true; //needed for non-solid blocks to count as walls
        TileID.Sets.HasOutlines[Type] = true;
        var name = CreateMapEntryName();
        name.SetDefault("Coughwood Door Open");
        AddMapEntry(new Color(119, 105, 79), name);
        disableSmartCursor = true;
        adjTiles = new int[] { TileID.OpenDoor };
        closeDoorID = Mod.Find<ModTile>("CoughwoodDoorClosed").Type;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }

    public override bool HasSmartInteract()
    {
        return true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = 1;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<CoughwoodDoor>());
    }

    public override void MouseOver(int i, int j)
    {
        var player = Main.LocalPlayer;
        player.noThrow = 2;
        player.showItemIcon = true;
        player.showItemIcon2 = ModContent.ItemType<CoughwoodDoor>();
    }
}