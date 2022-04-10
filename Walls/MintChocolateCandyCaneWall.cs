using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Walls;

public class MintChocolateCandyCaneWall : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        ItemDrop = ModContent.ItemType<MintChocolateCandyCaneWall>();
        AddMapEntry(new Color(160, 200, 47));
    }
}