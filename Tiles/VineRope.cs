using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class VineRope : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Lime);
        Main.tileLavaDeath[Type] = true;
        Main.tileRope[Type] = true;
        ItemDrop = ItemID.Vine;
        DustType = DustID.Grass;
    }
}
