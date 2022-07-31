using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Kunzite : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.HotPink, LanguageManager.Instance.GetText("Kunzite"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        ItemDrop = ModContent.ItemType<Items.Material.Kunzite>();
        HitSound = SoundID.Tink;
        MinPick = 210;
        DustType = DustID.PinkFairy;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
