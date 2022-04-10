using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TropicalGrassWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(35, 76, 0));
        SoundType = SoundID.Grass;
        SoundStyle = 1;
        DustType = DustID.GrassBlades;
    }
}
