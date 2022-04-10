using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TroxiniumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<TroxiniumBrickWall>();
        AddMapEntry(new Color(180, 88, 0));
        DustType = ModContent.DustType<Dusts.TroxiniumDust>();
    }
}