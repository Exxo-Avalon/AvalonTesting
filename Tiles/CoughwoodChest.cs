using AvalonTesting.Common;
using AvalonTesting.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class CoughwoodChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.CoughwoodChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Coughwood Chest");
        DustType = ModContent.DustType<ContagionDust>();
        base.SetStaticDefaults();
    }
}
