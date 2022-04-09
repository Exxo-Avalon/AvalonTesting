using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class Starbright : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Starbright");
        Description.SetDefault("Fallen stars fall more frequently");
    }
}
