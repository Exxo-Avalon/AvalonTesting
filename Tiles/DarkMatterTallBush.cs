using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles;

public class DarkMatterTallBush : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(100, 10, 120));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.CoordinateHeights = new[] {16, 16, 16};
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        HitSound = new Terraria.Audio.SoundStyle($"{nameof(Avalon)}/Sounds/Item/DarkMatterBushKill");
        DustType = ModContent.DustType<Dusts.DarkMatterDust>();
    }
    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
    {
        offsetY = 2;
    }
}
