using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class CoughwoodBeam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(57, 73, 47));
        ItemDrop = ModContent.ItemType<Items.Placeable.Beam.CoughwoodBeam>();
        TileID.Sets.IsBeam[Type] = true;
        DustType = ModContent.DustType<Dusts.ContagionDust>();
    }
}
