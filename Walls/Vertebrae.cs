using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class Vertebrae : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.VertebraeWall>();
        AddMapEntry(Color.LightCoral);
        DustType = DustID.HeartCrystal;
    }
}
