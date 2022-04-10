using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class AncientAdamantiteBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.AncientAdamantiteBrickWall>();
        AddMapEntry(new Color(148, 57, 101));
        DustType = DustID.Adamantite;
    }
}
