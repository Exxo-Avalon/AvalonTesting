using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class OrangeTiledWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWall>();
        AddMapEntry(new Color(107, 33, 0));
        DustType = DustID.Coralstone;
    }
}
