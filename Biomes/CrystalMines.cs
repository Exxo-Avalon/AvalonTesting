using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class CrystalMines : ModBiome
{
    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneCrystal;
    }
}
