using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonDresser : ModDresser
{
    protected override int DresserItemId => ModContent.ItemType<Items.Placeable.Storage.PurpleDungeonDresser>();
    protected override Color MapColor => new(191, 142, 111);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Purple Dungeon Dresser");
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
        base.SetStaticDefaults();
    }
}
