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
    public static bool IsOre(int type)
    {
        return type == TileID.Copper || type == TileID.Tin || type == ModContent.TileType<Tiles.Ores.BronzeOre>() ||
            type == TileID.Iron || type == TileID.Lead || type == ModContent.TileType<Tiles.Ores.NickelOre>() ||
            type == TileID.Silver || type == TileID.Tungsten || type == ModContent.TileType<Tiles.Ores.ZincOre>() ||
            type == TileID.Gold || type == TileID.Platinum || type == ModContent.TileType<Tiles.Ores.BismuthOre>() ||
            type == TileID.Demonite || type == TileID.Crimtane || type == ModContent.TileType<Tiles.Ores.BacciliteOre>() ||
            type == ModContent.TileType<Tiles.Ores.RhodiumOre>() || type == ModContent.TileType<Tiles.Ores.OsmiumOre>() ||
            type == ModContent.TileType<Tiles.Ores.IridiumOre>() || type == TileID.Hellstone || type == TileID.Cobalt ||
            type == TileID.Palladium || type == ModContent.TileType<Tiles.Ores.DurataniumOre>() || type == TileID.Mythril ||
            type == TileID.Orichalcum || type == ModContent.TileType<Tiles.Ores.NaquadahOre>() || type == TileID.Adamantite ||
            type == TileID.Titanium || type == ModContent.TileType<Tiles.Ores.TroxiniumOre>();
    }
    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        #region earthsplitter armor ore duping
        if (Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 16, 16)].GetModPlayer<ExxoPlayer>().oreDupe && IsOre(type) && Main.rand.NextBool(3))
        {
            int drop = 0;
            if (type == TileID.Copper) drop = ItemID.CopperOre;
            if (type == TileID.Tin) drop = ItemID.TinOre;
            if (type == ModContent.TileType<Tiles.Ores.BronzeOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.BronzeOre>();
            if (type == TileID.Iron) drop = ItemID.IronOre;
            if (type == TileID.Lead) drop = ItemID.LeadOre;
            if (type == ModContent.TileType<Tiles.Ores.NickelOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.NickelOre>();
            if (type == TileID.Silver) drop = ItemID.SilverOre;
            if (type == TileID.Tungsten) drop = ItemID.TungstenOre;
            if (type == ModContent.TileType<Tiles.Ores.ZincOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.ZincOre>();
            if (type == TileID.Gold) drop = ItemID.GoldOre;
            if (type == TileID.Platinum) drop = ItemID.PlatinumOre;
            if (type == ModContent.TileType<Tiles.Ores.BismuthOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.BismuthOre>();
            if (type == TileID.Demonite) drop = ItemID.DemoniteOre;
            if (type == TileID.Crimtane) drop = ItemID.CrimtaneOre;
            if (type == ModContent.TileType<Tiles.Ores.BacciliteOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.BacciliteOre>();
            if (type == ModContent.TileType<Tiles.Ores.RhodiumOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.RhodiumOre>();
            if (type == ModContent.TileType<Tiles.Ores.OsmiumOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.OsmiumOre>();
            if (type == ModContent.TileType<Tiles.Ores.IridiumOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.IridiumOre>();
            if (type == TileID.Cobalt) drop = ItemID.CobaltOre;
            if (type == TileID.Palladium) drop = ItemID.PalladiumOre;
            if (type == ModContent.TileType<Tiles.Ores.DurataniumOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.DurataniumOre>();
            if (type == TileID.Mythril) drop = ItemID.MythrilOre;
            if (type == TileID.Orichalcum) drop = ItemID.OrichalcumOre;
            if (type == ModContent.TileType<Tiles.Ores.NaquadahOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.NaquadahOre>();
            if (type == TileID.Adamantite) drop = ItemID.AdamantiteOre;
            if (type == TileID.Titanium) drop = ItemID.TitaniumOre;
            if (type == ModContent.TileType<Tiles.Ores.TroxiniumOre>()) drop = ModContent.ItemType<Items.Placeable.Tile.TroxiniumOre>();
            if (type == TileID.Hellstone) drop = ItemID.Hellstone;
            Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 16, drop);
        }
        #endregion earthsplitter armor ore duping
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
        if (type == ModContent.TileType<HallowedAltar>() && Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 16, 16)].inventory[Main.LocalPlayer.selectedItem].hammer < 100)
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
