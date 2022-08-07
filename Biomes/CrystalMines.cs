using Avalon.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Biomes;

public class CrystalMines : ModBiome
{
    public override int Music => Main.curMusic;
    public override bool IsBiomeActive(Player player)
    {
        return ModContent.GetInstance<Systems.BiomeTileCounts>().CrystalTiles > 150;
    }
}
