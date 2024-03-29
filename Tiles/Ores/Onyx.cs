using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Onyx : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(30, 30, 30), LanguageManager.Instance.GetText("Onyx"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        ItemDrop = ModContent.ItemType<Items.Material.Onyx>();
        MinPick = 210;
        HitSound = SoundID.Tink;
        DustType = DustID.Wraith;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
