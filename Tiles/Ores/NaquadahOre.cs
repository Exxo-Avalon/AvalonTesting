using ExxoAvalonOrigins.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Tiles.Ores;

public class NaquadahOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 3f;
        AddMapEntry(Color.Blue, LanguageManager.Instance.GetText("Naquadah"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileOreFinderPriority[Type] = 635;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 900;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tiles.NaquadahOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 110;
        DustType = ModContent.DustType<NaquadahDust>();
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
