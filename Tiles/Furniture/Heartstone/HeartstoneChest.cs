using Avalon.Common;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Heartstone;

public class HeartstoneChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.HeartstoneChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Heartstone Chest");
        DustType = ModContent.DustType<HeartstoneDust>();
        base.SetStaticDefaults();
    }
}
