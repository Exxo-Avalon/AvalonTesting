using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class RedVelCandyCane : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.GreenYellow);
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<RedVelvetCandyCaneBlock>();
    }
}
