using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PlatinumChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.PlatinumChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(162, 176, 183);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Platinum Chest");
        DustType = DustID.Platinum;
        base.SetStaticDefaults();
    }
}
