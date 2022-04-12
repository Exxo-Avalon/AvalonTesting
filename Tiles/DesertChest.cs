using AvalonTesting.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DesertChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.DesertChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Desert Chest");
        DustType = DustID.SandstormInABottle;
        base.SetStaticDefaults();
    }
}
