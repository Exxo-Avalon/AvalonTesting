using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class SkyBrickWallUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = false;
        AddMapEntry(new Color(79, 79, 59));
        ItemDrop = ModContent.ItemType<SkyBrickWall>();
        DustType = DustID.Smoke;
    }
}