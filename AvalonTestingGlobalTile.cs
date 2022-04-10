using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using AvalonTesting.Tiles;

namespace AvalonTesting;
public class AvalonTestingGlobalTile : GlobalTile
{
    public override void SetStaticDefaults()
    {
        int[] spelunkers =
        {
            TileID.Crimtane,
            TileID.Meteorite,
            TileID.Obsidian,
            TileID.Hellstone
        };
        foreach (var tile in spelunkers)
        {
            Main.tileSpelunker[tile] = true;
        }
    }
    public override void NearbyEffects(int i, int j, int type, bool closer)
    {
        if (type == ModContent.TileType<Tiles.Ores.PyroscoricOre>())
        {
            Dust.NewDust(new Vector2(j * 16, i * 16), 16, 16, DustID.InfernoFork, 0f, 0f);
        }
    }
    public override bool Slope(int i, int j, int type)
    {
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<Tiles.IckyAltar>() ||
            Main.tile[i, j - 1].TileType == ModContent.TileType<Tiles.HallowedAltar>())
        {
            return false;
        }
        return base.Slope(i, j, type);
    }
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (type == ModContent.TileType<ImperviousBrick>())
        {
            if (Main.tile[i, j - 1].TileType == ModContent.TileType<LockedImperviousDoor>() ||
                Main.tile[i, j + 1].TileType == ModContent.TileType<LockedImperviousDoor>())
            {
                fail = true;
            }
        }
        if (Main.tile[i, j - 1].TileType == ModContent.TileType<IckyAltar>() && Main.tile[i, j].TileType != ModContent.TileType<IckyAltar>() ||
            Main.tile[i, j - 1].TileType == ModContent.TileType<HallowedAltar>() && Main.tile[i, j].TileType != ModContent.TileType<HallowedAltar>())
        {
            fail = true;
        }
        if (type == TileID.Hellstone && Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].pick < 70)
        {
            fail = true;
        }
        if (type == TileID.Stalactite && Main.tile[i, j].TileFrameX < 54 && (Main.tile[i, j].TileFrameY == 0 || Main.tile[i, j].TileFrameY == 72) && Main.rand.Next(2) == 0)
        {
            int number2 = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Weapons.Throw.Icicle>(), 1, false, 0, false);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, NetworkText.FromLiteral(""), number2, 0f, 0f, 0f, 0);
                Main.item[number2].playerIndexTheItemIsReservedFor = Player.FindClosest(Main.item[number2].position, 8, 8);
            }
        }
    }
}
