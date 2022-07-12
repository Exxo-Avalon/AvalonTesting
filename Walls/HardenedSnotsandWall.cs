using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class HardenedSnotsandWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(57, 71, 41));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
        WallID.Sets.Conversion.HardenedSand[Type] = true;
    }
}
