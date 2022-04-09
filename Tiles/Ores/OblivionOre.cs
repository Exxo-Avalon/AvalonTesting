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
        mineResist = 8f;
        AddMapEntry(new Color(127, 0, 110), LanguageManager.Instance.GetText("Oblivion Ore"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 900;
        ItemDrop = Mod.Find<ModItem>("OblivionOre").Type;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        minPick = 300;
        DustType = DustID.Adamantine;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
