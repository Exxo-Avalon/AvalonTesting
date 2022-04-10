using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class PoisonSpike : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(95, 95, 36), LanguageManager.Instance.GetText("Poison Spike"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.PoisonSpike>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Grass;
    }
}
