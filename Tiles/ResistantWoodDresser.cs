using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class ResistantWoodDresser : ModDresser
{
    protected override int DresserItemId => ModContent.ItemType<Items.Placeable.Storage.ResistantWoodDresser>();
    protected override Color MapColor => new(191, 142, 111);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Resistant Wood Dresser");
        DustType = DustID.Wraith;
        base.SetStaticDefaults();
    }
}
