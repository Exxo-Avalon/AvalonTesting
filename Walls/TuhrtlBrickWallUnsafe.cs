using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class TuhrtlBrickWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(39, 31, 28));
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.TuhrtlBrickWall>();
        DustType = DustID.Silt;
    }
}
