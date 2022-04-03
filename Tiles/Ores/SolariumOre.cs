using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class SolariumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 4f;
        AddMapEntry(new Color(244, 167, 0), LanguageManager.Instance.GetText("Solarium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 730;
        Main.tileShine[Type] = 1150;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.SolariumOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 210;
        DustType = DustID.SolarFlare;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        r = 1.052549f;
        g = 0.720392168f;
        b = 0f;
    }
}
