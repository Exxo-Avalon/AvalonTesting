using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class ShadowScale : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.LightSteelBlue);
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileFrameImportant[Type] = true;
        ItemDrop = ItemID.ShadowScale;
        DustType = DustID.CorruptionThorns;
    }
}
