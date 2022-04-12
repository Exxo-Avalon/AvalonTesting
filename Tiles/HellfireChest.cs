using AvalonTesting.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class HellfireChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.HellfireChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Hellfire Chest");
        DustType = DustID.Wraith;
        base.SetStaticDefaults();
    }
}
