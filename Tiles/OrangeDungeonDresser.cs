using AvalonTesting.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class OrangeDungeonDresser : ModDresser
{
    protected override int DresserItemId => ModContent.ItemType<Items.Placeable.Storage.OrangeDungeonDresser>();
    protected override Color MapColor => new(191, 142, 111);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Orange Dungeon Dresser");
        DustType = DustID.Coralstone;
        base.SetStaticDefaults();
    }
}
