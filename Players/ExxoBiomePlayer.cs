using Avalon.Systems;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Players;

public class ExxoBiomePlayer : ModPlayer
{
    public bool ZoneContagion { get; private set; }
    public bool ZoneCaesium { get; private set; }
    public bool ZoneCrystal { get; private set; }
    public bool ZoneDarkMatter { get; private set; }
    public bool ZoneHellcastle { get; private set; }
    public bool ZoneNearHellcastle { get; private set; }
    public bool ZoneSkyFortress { get; private set; }
    public bool ZoneTropics { get; private set; }
    public bool ZoneTuhrtlOutpost { get; private set; }
    public bool ZoneUndergroundContagion { get; private set; }
    public bool ZoneTime { get; private set; }
    public bool ZoneBlight { get; private set; }
    public bool ZoneFright { get; private set; }
    public bool ZoneMight { get; private set; }
    public bool ZoneNight { get; private set; }
    public bool ZoneTorture { get; private set; }
    public bool ZoneIceSoul { get; private set; }
    public bool ZoneFlight { get; private set; }
    public bool ZoneHumidity { get; private set; }
    public bool ZoneDelight { get; private set; }
    public bool ZoneSight { get; private set; }

    public void UpdateZones(BiomeTileCounts biomeTileCounts)
    {
        Point tileCoordinates = Player.Center.ToTileCoordinates();
        ushort wallType = Main.tile[tileCoordinates.X, tileCoordinates.Y].WallType;

        ZoneContagion = biomeTileCounts.ContagionTiles > 200;
        ZoneUndergroundContagion = biomeTileCounts.ContagionTiles > 200 && Player.ZoneRockLayerHeight;
        ZoneCaesium = biomeTileCounts.CaesiumTiles > 200 && Player.ZoneUnderworldHeight;
        ZoneCrystal = biomeTileCounts.CrystalTiles > 100;
        ZoneDarkMatter = biomeTileCounts.DarkTiles > 450;
        ZoneNearHellcastle = biomeTileCounts.HellCastleTiles > 350 && Player.ZoneUnderworldHeight;
        ZoneHellcastle = biomeTileCounts.HellCastleTiles > 350 &&
                         wallType == ModContent.WallType<ImperviousBrickWallUnsafe>() && Player.ZoneUnderworldHeight;
        ZoneSkyFortress = biomeTileCounts.SkyFortressTiles > 50;
        ZoneTropics = biomeTileCounts.TropicsTiles > 50;
        ZoneTuhrtlOutpost = ZoneTropics && wallType == ModContent.WallType<TuhrtlBrickWallUnsafe>() &&
                            Player.ZoneRockLayerHeight;
        ZoneTime = biomeTileCounts.TimeTiles > 1;
        ZoneBlight = biomeTileCounts.BlightTiles > 1;
        ZoneFright = biomeTileCounts.FrightTiles > 1;
        ZoneMight = biomeTileCounts.MightTiles > 1;
        ZoneNight = biomeTileCounts.NightTiles > 1;
        ZoneTorture = biomeTileCounts.TortureTiles > 1;
        ZoneIceSoul = biomeTileCounts.IceSoulTiles > 1;
        ZoneFlight = biomeTileCounts.FlightTiles > 1;
        ZoneHumidity = biomeTileCounts.HumidityTiles > 1;
        ZoneDelight = biomeTileCounts.DelightTiles > 1;
        ZoneSight = biomeTileCounts.SightTiles > 1;
    }
}
