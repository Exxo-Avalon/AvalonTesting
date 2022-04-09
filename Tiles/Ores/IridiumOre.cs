using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class IridiumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(146, 167, 123), LanguageManager.Instance.GetText("Iridium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        Main.tileOreFinderPriority[Type] = 440;
        Main.tileSpelunker[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.IridiumOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.IridiumDust>();
    }
}
