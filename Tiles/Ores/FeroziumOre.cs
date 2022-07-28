using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class FeroziumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(new Color(0, 0, 250), LanguageManager.Instance.GetText("Ferozium"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 690;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.FeroziumOre>();
        HitSound = SoundID.Tink;
        MinPick = 180;
        TileID.Sets.Ore[Type] = true;
        DustType = DustID.UltraBrightTorch;
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
