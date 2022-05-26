using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class DemonSpikescale : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry((Color.Indigo), LanguageManager.Instance.GetText("Demon Spikescale"));
        Main.tileSolid[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.DemonSpikeScale>();
        DustType = DustID.CorruptionThorns;
        HitSound = SoundID.Tink;
    }
}
