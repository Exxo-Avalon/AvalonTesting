using AvalonTesting.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class DurataniumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(Color.Purple, LanguageManager.Instance.GetText("Duratanium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 615;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 925;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.DurataniumOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 100;
        DustType = ModContent.DustType<DurataniumDust>();
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
