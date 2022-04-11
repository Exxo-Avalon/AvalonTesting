using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.World.Structures;

class ObserverTemple
{
    public static void MakeObserverTemple(int x, int y)
    {
        int[,] _structure = {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2,2,2,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,1,1,1,3,2,2,3,3,3,2,2,3,1,1,1,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,1,3,3,3,2,3,3,3,2,3,3,3,2,3,3,3,1,1,1,0,0,0,0,0,0},
            {0,0,0,1,1,1,3,3,3,3,3,2,3,3,3,3,2,3,3,3,3,2,3,3,3,3,3,1,1,1,0,0,0},
            {1,1,1,1,1,1,1,1,1,3,3,3,2,3,3,3,2,3,3,3,2,3,3,3,1,1,1,1,1,1,1,1,1},
            {10,10,10,9,5,5,5,4,5,1,1,3,3,2,2,3,3,3,2,2,3,3,1,1,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,1,1,3,3,2,2,2,3,3,1,1,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,1,1,1,1,1,1,1,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,5,5,5,5,5,5,5,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,5,5,5,5,5,5,5,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,5,5,5,5,5,5,5,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,5,5,5,6,5,5,5,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {10,10,10,9,5,5,5,4,5,5,5,4,5,5,7,8,8,8,7,5,5,4,5,5,5,4,5,5,5,9,10,10,10},
            {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3}
        };
        int PosX = x;   //spawnX and spawnY is where you want the anchor to be when this generates
        int PosY = y;
        //i = vertical, j = horizontal
        for (int confirmPlatforms = 0; confirmPlatforms < 2; confirmPlatforms++)    //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
        {
            for (int i = 0; i < _structure.GetLength(0); i++)
            {
                for (int j = _structure.GetLength(1) - 1; j >= 0; j--)
                {
                    int k = PosX + j;
                    int l = PosY + i;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_structure[i, j])
                        {
                            case 0:
                                break;
                            case 1:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = TileID.PlatinumBrick;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 2:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.MoonplateBlock>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 3:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = TileID.MarbleBlock;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 4:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.PearlstoneColumn>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.BorealWood;
                                break;
                            case 5:
                                if (confirmPlatforms == 0)
                                {
                                    tile.HasTile = false;
                                    tile.LiquidAmount = 0;
                                    tile.IsHalfBlock = false;
                                    tile.Slope = SlopeType.Solid;
                                }
                                tile.LiquidAmount = 0;
                                tile.WallType = WallID.BorealWood;
                                break;
                            case 6:
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = SlopeType.Solid;
                                    tile.IsHalfBlock = false;
                                    WorldGen.PlaceTile(k, l, (ushort)ModContent.TileType<Tiles.Ancient.AncientWorkbench>(), true, true, -1, 0);
                                }
                                tile.LiquidAmount = 0;
                                tile.WallType = WallID.BorealWood;
                                break;
                            case 7:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = TileID.MarbleBlock;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = true;
                                tile.WallType = WallID.BorealWood;
                                break;
                            case 8:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = TileID.MarbleBlock;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.BorealWood;
                                break;
                            case 9:
                                tile.HasTile = true;
                                tile.LiquidAmount = 0;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.PearlstoneColumn>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 10:
                                tile.HasTile = false;
                                tile.LiquidAmount = 0;
                                tile.WallType = 0;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                        }
                    }
                }
            }
        }
    }
}
