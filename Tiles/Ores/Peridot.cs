using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class Peridot : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Lime, LanguageManager.Instance.GetText("Peridot"));
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Peridot>();
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 55;
        DustType = DustID.Grass;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
