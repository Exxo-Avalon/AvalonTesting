using AvalonTesting.Items.Weapons.Throw;
using AvalonTesting.Tiles;
using AvalonTesting.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AvalonTesting;

public class AvalonTestingGlobalTile : GlobalTile
{
    public override void SetStaticDefaults()
    {
        int[] spelunkers = {TileID.Crimtane, TileID.Meteorite, TileID.Obsidian, TileID.Hellstone};
        foreach (int tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
    }

    public override void NearbyEffects(int i, int j, int type, bool closer)
    {
        if (type == ModContent.TileType<PyroscoricOre>())
        {
            Dust.NewDust(new Vector2(j * 16, i * 16), 16, 16, DustID.InfernoFork);
        }
    }

    public override bool Slope(int i, int j, int type)
    {
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<IckyAltar>() ||
            Main.tile[i, j - 1].TileType == ModContent.TileType<HallowedAltar>())
        {
            return false;
        }

        return base.Slope(i, j, type);
    }

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        // Prevent Locked Impervious door from being broken from the block below
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<LockedImperviousDoor>() ||
            Main.tile[i, j + 1].TileType == ModContent.TileType<LockedImperviousDoor>())
        {
            fail = true;
        }

        // Sometimes drop icicles from ice stalactites
        if (type == TileID.Stalactite && Main.tile[i, j].TileFrameX < 54 &&
            Main.tile[i, j].TileFrameY is 0 or 72 && Main.rand.Next(2) == 0)
        {
            int number2 = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16,
                ModContent.ItemType<Icicle>());

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), number2);
                Main.item[number2].playerIndexTheItemIsReservedFor =
                    Player.FindClosest(Main.item[number2].position, 8, 8);
            }
        }
    }
}
