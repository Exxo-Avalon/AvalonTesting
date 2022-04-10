using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class NickelBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<NickelBrickWall>();
        AddMapEntry(new Color(52, 78, 85));
        DustType = ModContent.DustType<Dusts.NickelDust>();
    }
}