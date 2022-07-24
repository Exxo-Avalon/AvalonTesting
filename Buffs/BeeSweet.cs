using Terraria.ModLoader;

namespace Avalon.Buffs;

public class BeeSweet : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bee Sweet");
        Description.SetDefault("You are immune to Hornets");
    }
}
