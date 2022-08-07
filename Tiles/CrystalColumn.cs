using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class CrystalColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(154, 149, 247));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.CrystalPillar>();
        HitSound = SoundID.Tink;
        TileID.Sets.IsBeam[Type] = true;
        DustType = DustID.PinkCrystalShard;
        MinPick = 400;
    }
}
