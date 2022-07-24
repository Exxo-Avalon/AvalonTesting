using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class NickelFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.NickelFence>();
        DustType = ModContent.DustType<Dusts.NickelDust>();
    }
}
