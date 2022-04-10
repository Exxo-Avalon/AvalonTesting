using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class NaquadahBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<NaquadahBrickWall>();
        AddMapEntry(new Color(0, 0, 88));
        DustType = ModContent.DustType<Dusts.NaquadahDust>();
    }
}