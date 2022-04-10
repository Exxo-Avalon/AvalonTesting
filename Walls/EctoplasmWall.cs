using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class EctoplasmWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<EctoplasmWall>();
        AddMapEntry(new Color(0, 131, 181));
        DustType = DustID.Ultrabright;
    }
}