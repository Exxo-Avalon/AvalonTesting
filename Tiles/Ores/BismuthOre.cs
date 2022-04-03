using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class BismuthOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(187, 89, 192), LanguageManager.Instance.GetText("Bismuth"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1000;
        Main.tileOreFinderPriority[Type] = 275;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.BismuthOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
}
