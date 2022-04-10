using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class TopazGemWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<TopazGemWall>();
        AddMapEntry(new Color(97, 75, 28));
        DustType = DustID.Stone;
    }
}