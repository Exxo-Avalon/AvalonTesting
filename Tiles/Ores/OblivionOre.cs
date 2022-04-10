using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class OblivionOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 8f;
        AddMapEntry(new Color(127, 0, 110), LanguageManager.Instance.GetText("Oblivion Ore"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 900;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OblivionOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 300;
        DustType = DustID.Adamantite;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
