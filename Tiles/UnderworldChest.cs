﻿using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class UnderworldChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.UnderworldChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Underworld Chest");
        DustType = DustID.Wraith;
        base.SetStaticDefaults();
    }
}
