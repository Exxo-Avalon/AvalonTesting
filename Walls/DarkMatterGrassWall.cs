using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DarkMatterGrassWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(58, 37, 53));
        SoundType = SoundID.Grass;
        SoundStyle = 1;
        DustType = DustID.UnholyWater;
    }
}
