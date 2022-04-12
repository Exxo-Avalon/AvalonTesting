using AvalonTesting.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class ResistantWoodChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.ResistantWoodChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(50, 50, 50);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Resistant Wood Chest");
        DustType = DustID.Wraith;
        base.SetStaticDefaults();
    }
}
