﻿using Terraria;
using Terraria.ModLoader;

namespace Avalon.Buffs.AdvancedBuffs;

public class AdvHunter : ModBuff
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Advanced Hunter");
        Description.SetDefault("Shows the location of enemies");
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.detectCreature = true;
    }
}
