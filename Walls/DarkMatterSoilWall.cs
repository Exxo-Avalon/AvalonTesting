using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class DarkMatterSoilWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(103, 48, 84));
        DustType = DustID.UnholyWater;
    }
}
