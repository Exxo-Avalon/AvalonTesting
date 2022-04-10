using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class PyroscoricBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<PyroscoricBrickWall>();
        AddMapEntry(new Color(154, 40, 0));
        DustType = DustID.InfernoFork;
    }
}