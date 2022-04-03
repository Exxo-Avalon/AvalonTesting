using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class Heartstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(217, 2, 55), LanguageManager.Instance.GetText("Heartstone"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        //ItemDrop = ModContent.ItemType<>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Confetti_Pink;
    }
}
