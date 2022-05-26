using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class Tourmaline : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Aqua, LanguageManager.Instance.GetText("Tourmaline"));
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Tourmaline>();
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        HitSound = SoundID.Tink;
        MinPick = 55;
        DustType = DustID.Stone;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
