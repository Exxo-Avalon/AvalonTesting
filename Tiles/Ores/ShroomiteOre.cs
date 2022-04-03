using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class ShroomiteOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 5f;
        AddMapEntry(Color.CornflowerBlue, LanguageManager.Instance.GetText("Shroomite"));
        Main.tileSolid[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 705;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1400;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.ShroomiteOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 205;
        DustType = DustID.Clentaminator_Blue;
    }
}
