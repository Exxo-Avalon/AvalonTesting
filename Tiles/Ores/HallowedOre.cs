using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ores;

public class HallowedOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(new Color(219, 183, 0), LanguageManager.Instance.GetText("Hallowed Ore"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 690;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.HallowedOre>();
        HitSound = SoundID.Tink;
        MinPick = 185;
        DustType = DustID.Enchanted_Gold;
        TileID.Sets.Ore[Type] = true;
    }

    public override bool CanExplode(int i, int j)
    {
        return false;
    }
}
