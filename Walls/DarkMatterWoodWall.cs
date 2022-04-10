using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DarkMatterWoodWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<DarkMatterWoodWall>();
        AddMapEntry(new Color(56, 40, 63));
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
    }
}