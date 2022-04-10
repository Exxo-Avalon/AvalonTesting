using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DiamondGemWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<DiamondGemWall>();
        AddMapEntry(new Color(40, 105, 114));
        DustType = DustID.Stone;
    }
}