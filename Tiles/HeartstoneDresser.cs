using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class HeartstoneDresser : ModDresser
{
    protected override int DresserItemId => ModContent.ItemType<Items.Placeable.Storage.HeartstoneDresser>();
    protected override Color MapColor => new(191, 142, 111);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Heartstone Dresser");
        DustType = DustID.Confetti_Pink;
        base.SetStaticDefaults();
    }
}
