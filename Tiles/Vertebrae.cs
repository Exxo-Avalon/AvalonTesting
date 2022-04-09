using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class Vertebrae : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(255, 127, 127));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = ItemID.Vertebrae;
        SoundType = SoundID.NPCHit;
        SoundStyle = 2;
        DustType = DustID.HeartCrystal;
    }
}
