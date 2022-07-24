using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class DarkMatterGrassWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(58, 37, 53));
        HitSound = SoundID.Grass;
        DustType = DustID.UnholyWater;
        WallID.Sets.Conversion.Grass[Type] = true;
    }
}
