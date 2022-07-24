using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class RedVelvetCandyCaneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.RedVelvetCandyCaneWall>();
        AddMapEntry(new Color(180, 80, 80));
    }
}
