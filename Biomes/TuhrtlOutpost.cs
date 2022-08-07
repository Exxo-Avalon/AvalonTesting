using Microsoft.Xna.Framework;
using Avalon.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Walls;

namespace Avalon.Biomes;

public class TuhrtlOutpost : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

    public override int Music => Avalon.MusicMod != null ? MusicLoader.GetMusicSlot(Avalon.MusicMod, "Sounds/Music/TuhrtlOutpost") : MusicID.Temple;

    public override bool IsBiomeActive(Player player)
    {
        Point tileCoordinates = player.Center.ToTileCoordinates();
        ushort wallType = Main.tile[tileCoordinates.X, tileCoordinates.Y].WallType;
        return ModContent.GetInstance<Systems.BiomeTileCounts>().TropicsTiles > 200 && wallType == ModContent.WallType<TuhrtlBrickWallUnsafe>();
    }
}
