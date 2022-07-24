using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class RhodiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.RhodiumBrickWall>();
        AddMapEntry(new Color(159, 117, 124));
        DustType = DustID.t_LivingWood;
    }
}
