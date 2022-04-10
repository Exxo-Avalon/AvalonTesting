using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DurataniumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<DurataniumBrickWall>();
        AddMapEntry(new Color(91, 0, 54));
        DustType = DustType = ModContent.DustType<Dusts.DurataniumDust>();
    }
}