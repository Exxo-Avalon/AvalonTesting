using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Magnet : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Magnet");
        Description.SetDefault("Item grab range is increased");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.treasureMagnet = true;
    }
}
