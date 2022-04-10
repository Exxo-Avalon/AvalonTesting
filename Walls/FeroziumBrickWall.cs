using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class FeroziumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<FeroziumBrickWall>();
        AddMapEntry(new Color(0, 123, 200));
    }
}