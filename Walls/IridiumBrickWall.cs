using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class IridiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.IridiumBrickWall>();
        AddMapEntry(new Color(95, 116, 72));
        DustType = ModContent.DustType<Dusts.IridiumDust>();
    }
}
