using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class DarkSlimeChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.DarkSlimeChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Dark Slime Chest");
        DustType = DustID.UnholyWater;
        base.SetStaticDefaults();
    }
}
