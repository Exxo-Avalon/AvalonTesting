using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class RainbowCandyCane : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Indigo);
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<RainbowCandyCaneBlock>();
    }
}
