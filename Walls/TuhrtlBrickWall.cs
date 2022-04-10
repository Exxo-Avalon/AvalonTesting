using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TuhrtlBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(39, 31, 28));
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<TuhrtlBrickWall>();
        DustType = DustID.Silt;
    }
}