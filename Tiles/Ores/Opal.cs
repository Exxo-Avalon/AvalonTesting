using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class Opal : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(165, 255, 127), LanguageManager.Instance.GetText("Opal"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Material.Opal>();
        Main.tileSpelunker[Type] = true;
        Main.tileMerge[TileID.Stone][Type] = true;
        Main.tileMerge[Type][TileID.Stone] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        HitSound = SoundID.Tink;
        MinPick = 210;
        DustType = DustID.Stone;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
