using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class SkyBrickWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(79, 79, 59));
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.SkyBrickWall>();
        DustType = DustID.Smoke;
    }
}
