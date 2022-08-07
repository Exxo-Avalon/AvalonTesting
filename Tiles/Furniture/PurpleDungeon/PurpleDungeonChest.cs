using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.PurpleDungeon;

public class PurpleDungeonChest : ModChest
{
    protected override int ChestItemId => ModContent.ItemType<Items.Placeable.Storage.PurpleDungeonChest>();
    protected override bool CanBeLocked => false;
    protected override Color UnlockedMapColor => new(174, 129, 92);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Purple Dungeon Chest");
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
        base.SetStaticDefaults();
    }
}
