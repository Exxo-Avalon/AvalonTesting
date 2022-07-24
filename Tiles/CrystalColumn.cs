using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CrystalColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(73, 51, 36));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.CrystalColumn>();
        HitSound = SoundID.Tink;
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.PinkCrystalShard;
    }
}
