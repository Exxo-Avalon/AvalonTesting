using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Tiles;

public class FallenStarTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.LightYellow);
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;
        //TileID.Sets.Platforms[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.UsesCustomCanPlace = false;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);
        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
        TileID.Sets.DisableSmartCursor[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.FallenStarBlock>();
        DustType = DustID.GemTopaz;
    }
}
