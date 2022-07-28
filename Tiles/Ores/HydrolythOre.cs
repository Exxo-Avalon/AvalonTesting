using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class HydrolythOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 8f;
        AddMapEntry(new Color(0, 255, 255), LanguageManager.Instance.GetText("Hydrolyth"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.HydrolythOre>();
        HitSound = SoundID.Tink;
        MinPick = 300;
        DustType = DustID.MagicMirror;
        TileID.Sets.Ore[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
