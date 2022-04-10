using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class CoughwoodWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<CoughwoodWall>();
        AddMapEntry(new Color(106, 116, 90));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}