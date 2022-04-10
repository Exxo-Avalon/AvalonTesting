using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ChlorophyteBrick : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<ChlorophyteBrickWall>();
        AddMapEntry(new Color(10, 200, 20));
        DustType = DustID.Chlorophyte;
    }
}