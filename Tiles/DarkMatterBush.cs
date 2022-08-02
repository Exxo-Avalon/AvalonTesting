using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class DarkMatterBush : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(100, 10, 120));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.CoordinateHeights = new[] {16, 16};
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        HitSound = SoundID.Grass;
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
    }
}
