using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace AvalonTesting.Walls;

public class CrystalStoneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(86, 51, 76));
        DustType = DustID.PinkCrystalShard;
    }
    //public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    //{
    //    //spriteBatch.Draw();
    //}
}