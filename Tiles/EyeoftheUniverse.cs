using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class EyeoftheUniverse : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
        TileObjectData.newTile.Width = 12;
        TileObjectData.newTile.Height = 9;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16, 16, 16, 16, 16, 16, 16, 16, 16
        };
        TileObjectData.newTile.AnchorWall = true;
        TileObjectData.addTile(Type);
        DustType = 7;
        TileID.Sets.DisableSmartCursor[Type] = true;
        ModTranslation name = CreateMapEntryName();
        name.SetDefault("Painting");
        AddMapEntry(new Color(120, 85, 60), name);
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 48, 48, ModContent.ItemType<Items.Placeable.Painting.EyeoftheUniverse>());
    }
}
