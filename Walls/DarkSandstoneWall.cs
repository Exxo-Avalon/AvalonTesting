using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DarkSandstoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(58, 37, 53));
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
        WallID.Sets.Conversion.Sandstone[Type] = true;
    }
}
