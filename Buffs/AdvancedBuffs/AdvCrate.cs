using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvCrate : ModBuff
{
    public const float Chance = 0.15f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Crate");
        Description.SetDefault("Greater chance of fishing up a crate");
    }
}
