using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ApocalyptusWoodWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ApocalyptusWoodWall>();
        AddMapEntry(new Color(56, 40, 63));
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
    }
}
