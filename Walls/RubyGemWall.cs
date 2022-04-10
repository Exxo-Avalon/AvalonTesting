using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class RubyGemWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<RubyGemWall>();
        AddMapEntry(new Color(125, 50, 64));
        DustType = DustID.Stone;
    }
}