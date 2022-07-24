using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class PyroscoricBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.PyroscoricBrickWall>();
        AddMapEntry(new Color(154, 40, 0));
        DustType = DustID.InfernoFork;
    }
}
