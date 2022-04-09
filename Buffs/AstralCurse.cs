using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Buffs;

public class AstralCurse : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Astral Curse");
        Description.SetDefault("You take triple damage");
        Main.debuff[Type] = true;
    }
}
