using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class BronzeBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<BronzeBrickWall>();
        AddMapEntry(new Color(61, 29, 26));
        DustType = ModContent.DustType<Dusts.BronzeDust>();
    }
}