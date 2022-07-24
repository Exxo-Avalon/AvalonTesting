using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class PearlstoneColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(73, 51, 36));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.PearlstoneColumn>();
        TileID.Sets.IsBeam[Type] = true;
        HitSound = SoundID.Tink;
    }
}
