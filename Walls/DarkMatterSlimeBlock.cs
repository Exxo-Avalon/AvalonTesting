using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class DarkMatterSlimeBlock : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.DarkSlimeBlockWall>();
        AddMapEntry(new Color(51, 0, 96));
        DustType = DustID.UnholyWater;
    }
}
