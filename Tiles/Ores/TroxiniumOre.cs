using ExxoAvalonOrigins.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Tiles.Ores;

public class TroxiniumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 4f;
        AddMapEntry(Color.Goldenrod, LanguageManager.Instance.GetText("Troxinium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 660;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 875;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tiles.TroxiniumOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 150;
        DustType = ModContent.DustType<TroxiniumDust>();
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
