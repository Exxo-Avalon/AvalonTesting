using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class ImmortalityCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Immortality Cooldown");
        Description.SetDefault("You are not immortal");
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }
}
