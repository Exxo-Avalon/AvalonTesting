using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class RainbowCandyCaneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<RainbowCandyCaneWall>();
        AddMapEntry(new Color(60, 0, 100));
    }
}