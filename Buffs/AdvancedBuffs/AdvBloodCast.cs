﻿using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvBloodCast : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Blood Casting");
        Description.SetDefault("Maximum life is added to maximum mana");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.statManaMax2 += player.statLifeMax2;
    }
}
