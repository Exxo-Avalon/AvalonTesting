using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture.Ectoplasm;

public class EctoplasmDresser : ModDresser
{
    protected override int DresserItemId => ModContent.ItemType<Items.Placeable.Storage.EctoplasmDresser>();
    protected override Color MapColor => new(191, 142, 111);

    public override void SetStaticDefaults()
    {
        ContainerName.SetDefault("Ectoplasm Dresser");
        DustType = DustID.UltraBrightTorch;
        base.SetStaticDefaults();
    }
}
