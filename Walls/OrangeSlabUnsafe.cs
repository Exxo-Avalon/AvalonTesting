using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class OrangeSlabUnsafe : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.OrangeSlabWall>();
        AddMapEntry(new Color(107, 33, 0));
        DustType = DustID.Coralstone;
    }
}
