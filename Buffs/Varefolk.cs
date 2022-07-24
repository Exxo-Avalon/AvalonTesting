using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Buffs;

public class Varefolk : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Varefolk");
        Description.SetDefault("Immune to lava and can move easily in it");
        Main.debuff[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }
}
