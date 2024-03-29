﻿using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvSonar : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Sonar");
        Description.SetDefault("You can see what's biting your hook");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.sonarPotion = true;
    }
}
