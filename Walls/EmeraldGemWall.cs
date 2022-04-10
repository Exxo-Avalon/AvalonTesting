using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class EmeraldGemWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<EmeraldGemWall>();
        AddMapEntry(new Color(26, 97, 58));
        DustType = DustID.Stone;
    }
}