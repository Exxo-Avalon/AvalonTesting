using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TuhrtlBrickWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(39, 31, 28));
        ItemDrop = ModContent.ItemType<TuhrtlBrickWall>();
        DustType = DustID.Silt;
    }
}