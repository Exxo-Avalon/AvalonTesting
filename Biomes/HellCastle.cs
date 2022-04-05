using AvalonTesting.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Biomes;

public class HellCastle : ModBiome
{
    public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    public override int Music
    {
        get
        {
            if (ModLoader.TryGetMod("AvalonMusic", out AvalonTesting.Mod.MusicMod))
            {
                return MusicLoader.GetMusicSlot(AvalonTesting.Mod.MusicMod, "Sounds/Music/Hellcastle"); 
            }
            return MusicID.Dungeon;
        }
    }

    public override bool IsBiomeActive(Player player)
    {
        return player.GetModPlayer<ExxoBiomePlayer>().ZoneHellcastle;
    }
}
