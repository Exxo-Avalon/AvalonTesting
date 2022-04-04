using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class VenomSpike : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(132, 65, 172), LanguageManager.Instance.GetText("Venom Spike"));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeables.Tile.VenomSpike>();
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
    public override bool Slope(int i, int j)
    {
        return false;
    }
}
