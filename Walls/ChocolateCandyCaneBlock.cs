using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ChocolateCandyCaneBlock : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<ChocolateCandyCaneWall>();
        AddMapEntry(Color.SaddleBrown);
    }
}