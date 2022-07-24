using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class MushroomTile : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(237, 160, 69));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMergeDirt[Type] = true;
        ItemDrop = ItemID.Mushroom;
        DustType = DustID.GemAmber;
    }
}
