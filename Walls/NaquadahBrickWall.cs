using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NaquadahBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.NaquadahBrickWall>();
        AddMapEntry(new Color(0, 0, 88));
        DustType = ModContent.DustType<Dusts.NaquadahDust>();
    }
}
