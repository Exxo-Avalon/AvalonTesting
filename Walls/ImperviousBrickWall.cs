using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class ImperviousBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        AddMapEntry(new Color(51, 44, 48));
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.ImperviousBrickWall>();
        DustType = DustID.Wraith;
    }
}
