using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles.Ores;

public class RhodiumOre : ModTile
{
    public override void SetStaticDefaults()
    {
        MineResist = 2f;
        AddMapEntry(new Color(142, 91, 91), LanguageManager.Instance.GetText("Rhodium"));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileSpelunker[Type] = true;
        Main.tileOreFinderPriority[Type] = 420;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 1150;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.RhodiumOre>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 60;
        DustType = DustID.t_LivingWood;
    }
}
