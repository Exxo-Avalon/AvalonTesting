using AvalonTesting.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class MintChocCandyCane : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.IndianRed);
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<MintChocolateCandyCaneBlock>();
    }
}
