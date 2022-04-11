using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class SulphurOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(218, 216, 114), LanguageManager.Instance.GetText("Sulphur"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.Sulphur>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Enchanted_Gold;
    }
}
