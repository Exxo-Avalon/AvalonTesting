using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Walls;

public class EctoplasmWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Wall.EctoplasmWall>();
        AddMapEntry(new Color(0, 131, 181));
        DustType = DustID.UltraBrightTorch;
    }
}
