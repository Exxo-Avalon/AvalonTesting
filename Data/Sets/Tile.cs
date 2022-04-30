using AvalonTesting.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Data.Sets;

public static class Tile
{
    public static readonly bool[] NoHammerTileBelow = TileID.Sets.Factory.CreateBoolSet(
        ModContent.TileType<IckyAltar>(),
        ModContent.TileType<HallowedAltar>()
    );

    public static readonly bool[] Altar = TileID.Sets.Factory.CreateBoolSet(
        ModContent.TileType<IckyAltar>(),
        ModContent.TileType<HallowedAltar>()
    );
}
