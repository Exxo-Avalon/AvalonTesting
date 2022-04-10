using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class AncientMythrilBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.AncientMythrilBrickWall>();
        AddMapEntry(new Color(60, 91, 58));
        DustType = DustID.Mythril;
    }
}
