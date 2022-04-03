﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Tiles.Ores;

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
        //ItemDrop = ModContent.ItemType<>();
        MinPick = 210;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Wraith;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
