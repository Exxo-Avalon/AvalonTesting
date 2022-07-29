using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class ChunkstoneColumn : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(96, 116, 75));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.ChunkstoneColumn>();
        TileID.Sets.IsBeam[Type] = true;
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
