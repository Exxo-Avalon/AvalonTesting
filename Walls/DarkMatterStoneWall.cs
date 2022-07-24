using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class DarkMatterStoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(51, 4, 88));
        HitSound = SoundID.NPCDeath1;
        DustType = DustID.UnholyWater;
        WallID.Sets.Conversion.Stone[Type] = true;
    }
}
