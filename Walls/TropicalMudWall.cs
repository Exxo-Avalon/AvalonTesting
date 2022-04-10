using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TropicalMudWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(67, 32, 20));
        DustType = ModContent.DustType<Dusts.TropicalMudDust>();
    }
}