using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class AncientCobaltBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.AncientCobaltBrickWall>();
        AddMapEntry(new Color(22, 53, 80));
        DustType = DustID.Cobalt;
    }
}
