using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class HydrolythOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 8f;
        AddMapEntry(new Color(0, 255, 255), LanguageManager.Instance.GetText("Hydrolyth"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.HydrolythOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 300;
        DustType = DustID.MagicMirror;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
