using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class HeartstoneBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(173, 0, 38));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = Mod.Find<ModItem>("HeartstoneBrick").Type;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        DustType = DustID.Confetti_Pink;
    }
}
