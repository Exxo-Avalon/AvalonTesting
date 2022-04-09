using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class Starstone : ModTile
{
    private Color starstoneColor = new Color(42, 102, 221);
    public override void SetStaticDefaults()
    {
        AddMapEntry(starstoneColor, LanguageManager.Instance.GetText("Starstone"));
        Main.tileSolid[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 775;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Starstone>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.BlueTorch;
    }
}
