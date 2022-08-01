using Avalon.Items.Weapons.Throw;
using Avalon.Players;
using Avalon.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon;

public class AvalonGlobalTile : GlobalTile
{
    public override void SetStaticDefaults()
    {
        int[] spelunkers = { TileID.Crimtane, TileID.Meteorite, TileID.Obsidian, TileID.Hellstone };
        foreach (int tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
    }
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 16, 16)].GetModPlayer<ExxoPlayer>().oreDupe && TileID.Sets.Ore[Main.tile[i, j].TileType])
        {
            if (Data.Sets.Tile.OresToChunks.ContainsKey(Main.tile[i, j].TileType))
            {
                int drop = Data.Sets.Tile.OresToChunks[Main.tile[i, j].TileType];
                if (Main.rand.NextBool(3))
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                }
                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
                noItem = true;
            }
        }
        // Prevent Locked Impervious door from being broken from the block below or above
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<LockedImperviousDoor>() ||
            Main.tile[i, j + 1].TileType == ModContent.TileType<LockedImperviousDoor>())
        {
            fail = true;
        }
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<IckyAltar>() && Main.tile[i, j].TileType != ModContent.TileType<IckyAltar>() ||
            Main.tile[i, j - 1].TileType == ModContent.TileType<HallowedAltar>() && Main.tile[i, j].TileType != ModContent.TileType<HallowedAltar>())
        {
            fail = true;
        }
        if (type == TileID.Hellstone && Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 16, 16)].inventory[Main.LocalPlayer.selectedItem].pick < 70)
        {
            fail = true;
        }
        if (type == ModContent.TileType<HallowedAltar>() && Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 16, 16)].inventory[Main.LocalPlayer.selectedItem].hammer < 120)
        {
            fail = true;
        }
        // Sometimes drop icicles from ice stalactites
        if (type == TileID.Stalactite && Main.tile[i, j].TileFrameX < 54 &&
            Main.tile[i, j].TileFrameY is 0 or 72 && Main.rand.NextBool(2))
        {
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16,
                ModContent.ItemType<Icicle>());
        }
    }

    public override bool Slope(int i, int j, int type)
    {
        if (Main.tile[i, j - 1].HasTile && Data.Sets.Tile.NoHammerTileBelow[Main.tile[i, j - 1].TileType])
        {
            return false;
        }

        return base.Slope(i, j, type);
    }
}
