using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class BrimstoneBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(165, 80, 98));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Impgrass>()] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BrimstoneBlock>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.HeartCrystal;
        TileID.Sets.HellSpecial[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
    }
}
