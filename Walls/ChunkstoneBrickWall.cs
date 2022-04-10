using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ChunkstoneBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<ChunkstoneBrickWall>();
        AddMapEntry(new Color(67, 83, 61));
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}