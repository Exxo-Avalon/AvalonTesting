using Avalon.Common;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Both)]
public class DungeonRework : ModHook
{
    protected override void Apply()
    {
        On.Terraria.WorldGen.DungeonPitTrap += OnDungeonPitTrap;
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<AvalonConfig>().RevertDungeonGen;
    }
    private static bool OnDungeonPitTrap(On.Terraria.WorldGen.orig_DungeonPitTrap orig, int i, int j, ushort tileType, int wallType)
    {
        return true;
    }
}
public class DungeonRemoveCrackedBricks : GenPass
{
    public DungeonRemoveCrackedBricks()
        : base("DungeonRemoveCrackedBricks", 10)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        for (int i = 100; i < Main.maxTilesX - 100; i++)
        {
            for (int j = 100; j < Main.maxTilesY - 200; j++)
            {
                if ((Main.tile[i, j].TileType is TileID.CrackedBlueDungeonBrick or TileID.CrackedGreenDungeonBrick or TileID.CrackedPinkDungeonBrick) &&
                    Main.tile[i, j].HasTile && ModContent.GetInstance<AvalonConfig>().RevertDungeonGen)
                {
                    WorldGen.KillTile(i, j);
                }
                if ((Main.tile[i, j].TileType is TileID.CrackedBlueDungeonBrick or TileID.CrackedGreenDungeonBrick or TileID.CrackedPinkDungeonBrick) &&
                    Main.tile[i, j].HasTile && !ModContent.GetInstance<AvalonConfig>().RevertDungeonGen)
                {
                    Tile t = Main.tile[i, j];
                    t.HasTile = false;
                    WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.CrackedOrangeBrick>(), true);
                }
                if (WorldGen.genRand.NextBool(1))
                {
                    if (Main.tile[i, j].TileType is TileID.BlueDungeonBrick or TileID.GreenDungeonBrick or TileID.PinkDungeonBrick &&
                        Main.tile[i, j].HasTile)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.OrangeBrick>();
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonUnsafe or WallID.BlueDungeonUnsafe or
                        WallID.PinkDungeonUnsafe)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeBrickUnsafe>();
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonSlabUnsafe or WallID.BlueDungeonSlabUnsafe or
                        WallID.PinkDungeonSlabUnsafe)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeSlabUnsafe>();
                    }
                    if (Main.tile[i, j].WallType is WallID.GreenDungeonTileUnsafe or WallID.BlueDungeonTileUnsafe or
                        WallID.PinkDungeonTileUnsafe)
                    {
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<Walls.OrangeTiledUnsafe>();
                    }
                    if (Main.tile[i, j].TileType is TileID.Platforms && Main.tile[i, j].TileFrameY is 6 * 18 or 7 * 18 or 8 * 18 &&
                        Main.tile[i, j].HasTile)
                    {
                        Main.tile[i, j].TileType = (ushort)ModContent.TileType<Tiles.OrangeBrickPlatform>();
                        Main.tile[i, j].TileFrameY = 0;
                    }
                    if (Main.tile[i, j].TileType is TileID.Bathtubs && Main.tile[i, j].TileFrameY is >= 756 and <= 846 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonBathtub>(), true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Beds && Main.tile[i, j].TileFrameY is >= 180 and <= 270 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonBed>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Bookcases && Main.tile[i, j].TileFrameX is >= 54 and <= 198 &&
                        Main.tile[i, j].TileFrameY <= 54 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<Tiles.OrangeDungeonBookcase>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Candles && Main.tile[i, j].TileFrameY is >= 22 and <= 66 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.OrangeDungeonCandle>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Candelabras && Main.tile[i, j].TileFrameY is >= 792 and <= 882 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.OrangeDungeonCandelabra>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Chairs && Main.tile[i, j].TileFrameY is >= 520 and <= 618 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 3, ModContent.TileType<Tiles.OrangeDungeonChair>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Chandeliers && Main.tile[i, j].TileFrameY is >= 1458 and <= 1602 &&
                        Main.tile[i, j].TileFrameX <= 90 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j, ModContent.TileType<Tiles.OrangeDungeonChandelier>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.ClosedDoor && Main.tile[i, j].TileFrameY is >= 864 and <= 1008 &&
                        Main.tile[i, j].TileFrameX <= 36 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j + 1, ModContent.TileType<Tiles.OrangeDungeonDoorClosed>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Dressers && Main.tile[i, j].TileFrameX is >= 270 and <= 414 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonDresser>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Lamps && Main.tile[i, j].TileFrameY is >= 1300 and <= 1440 &&
                        Main.tile[i, j].TileFrameX <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j + 2, ModContent.TileType<Tiles.OrangeDungeonLamp>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Pianos && Main.tile[i, j].TileFrameX is >= 594 and <= 738 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonPiano>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Sinks && Main.tile[i, j].TileFrameY is >= 380 and <= 474 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.OrangeDungeonSink>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Benches && Main.tile[i, j].TileFrameX is >= 324 and <= 468 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonSofa>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Tables && Main.tile[i, j].TileFrameX is >= 540 and <= 684 &&
                        Main.tile[i, j].TileFrameY <= 18 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i + 1, j + 1, ModContent.TileType<Tiles.OrangeDungeonTable>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.WorkBenches && Main.tile[i, j].TileFrameX is >= 396 and <= 486 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j, ModContent.TileType<Tiles.OrangeDungeonWorkbench>(), mute: true);
                    }
                    if (Main.tile[i, j].TileType is TileID.Statues && Main.tile[i, j].TileFrameX is >= 1656 and <= 1746 &&
                        Main.tile[i, j].TileFrameY <= 36 && Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j + 2, ModContent.TileType<Tiles.Statues>(), mute: true, style: 3);
                    }
                    if (Main.tile[i, j].TileType is TileID.GrandfatherClocks && Main.tile[i, j].TileFrameX is >= 1080 and <= 1152 &&
                        Main.tile[i, j].HasTile)
                    {
                        WorldGen.KillTile(i, j);
                        WorldGen.PlaceTile(i, j + 4, ModContent.TileType<Tiles.OrangeDungeonClock>(), mute: true);
                    }
                }
            }
        }
    }
}
