using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ZincBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<ZincBrickWall>();
        AddMapEntry(new Color(76, 65, 75));
        DustType = ModContent.DustType<Dusts.ZincDust>();
    }
}