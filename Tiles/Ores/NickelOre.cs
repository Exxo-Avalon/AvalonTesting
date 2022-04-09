using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class NickelOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(82, 112, 122), LanguageManager.Instance.GetText("Nickel"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1050;
        Main.tileOreFinderPriority[Type] = 235;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.NickelOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.NickelDust>();
    }
}
