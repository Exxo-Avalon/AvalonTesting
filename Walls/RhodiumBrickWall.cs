using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class RhodiumBrickWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<RhodiumBrickWall>();
        AddMapEntry(new Color(159, 117, 124));
        DustType = DustID.t_LivingWood;
    }
}