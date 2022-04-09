using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AvalonTesting.Tiles;

public class OrangeDungeonBed : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = true;
        TileID.Sets.HasOutlines[Type] = true;
        TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
        TileObjectData.newTile.CoordinateHeights = new[] {16, 18};
        TileObjectData.addTile(Type);
        ModTranslation name = CreateMapEntryName();
        name.SetDefault("Orange Dungeon Bed");
        AddMapEntry(new Color(191, 142, 111), name);
        disableSmartCursor = true;
        adjTiles = new int[] {TileID.Beds};
        bed = true;
        DustType = DustID.Coralstone;
    }

    public override bool HasSmartInteract()
    {
        return true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(i * 16, j * 16, 64, 32, ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeonBed>());
    }

    public override void RightClick(int i, int j)
    {
        Player player = Main.LocalPlayer;
        Tile tile = Main.tile[i, j];
        int spawnX = i - (tile.TileFrameX / 18);
        int spawnY = j + 2;
        spawnX += tile.TileFrameX >= 72 ? 5 : 2;
        if (tile.TileFrameY % 38 != 0)
        {
            spawnY--;
        }

        player.FindSpawn();
        if (player.SpawnX == spawnX && player.SpawnY == spawnY)
        {
            player.RemoveSpawn();
            Main.NewText("Spawn point removed!", 255, 240, 20, false);
        }
        else if (Player.CheckSpawn(spawnX, spawnY))
        {
            player.ChangeSpawn(spawnX, spawnY);
            Main.NewText("Spawn point set!", 255, 240, 20, false);
        }
    }

    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.showItemIcon = true;
        player.showItemIcon2 = ModContent.ItemType<Items.Placeable.Furniture.OrangeDungeonBed>();
    }
}
