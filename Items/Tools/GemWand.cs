using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Avalon.Items.Material;
using Avalon.Items.Placeable.Tile;

namespace Avalon.Items.Tools;

class GemWand : ModItem
{
    public static readonly int[] Gems =
    {
        ItemID.Topaz,
        ItemID.Ruby,
        ItemID.Amethyst,
        ItemID.Diamond,
        ItemID.Emerald,
        ItemID.Sapphire,
        ModContent.ItemType<Onyx>(),
        ModContent.ItemType<Zircon>(),
        ModContent.ItemType<Kunzite>(),
        ModContent.ItemType<Peridot>(),
        ModContent.ItemType<Tourmaline>(),
        ModContent.ItemType<Opal>()
    };
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Gem Wand");
        Tooltip.SetDefault("Places ore-form gems");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.IronBar, 6).AddIngredient(ItemID.Sapphire).AddIngredient(ItemID.Ruby).AddIngredient(ItemID.Emerald).AddIngredient(ItemID.Topaz).AddIngredient(ItemID.Amethyst).AddIngredient(ItemID.Diamond).AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override bool? UseItem(Player player)
    {
        Vector2 mousePosition = Main.MouseWorld;
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            player.Avalon().MousePosition = mousePosition;
            Network.CursorPosition.SendPacket(mousePosition, player.whoAmI);
        }
        else if (Main.netMode == NetmodeID.SinglePlayer)
        {
            player.Avalon().MousePosition = mousePosition;
        }
        Point mpTile = player.Avalon().MousePosition.ToTileCoordinates();
        if (Main.myPlayer == player.whoAmI)
        {
            for (int q = 0; q < player.inventory.Length; q++)
            {
                int t = player.inventory[q].type;
                bool inrange = (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= mpTile.X &&
                                (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= mpTile.X &&
                                player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= mpTile.Y &&
                                (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= mpTile.Y);
                if (Gems.Contains(t) && t != 0)
                {
                    if (!Main.tile[mpTile.X, mpTile.Y].HasTile && inrange)
                    {
                        bool subtractFromStack = WorldGen.PlaceTile(mpTile.X, mpTile.Y, GemToTile(t));
                        if (Main.tile[mpTile.X, mpTile.Y].HasTile && Main.netMode != NetmodeID.SinglePlayer && subtractFromStack)
                        {
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, mpTile.X, mpTile.Y, GemToTile(t));
                        }
                        if (subtractFromStack)
                        {
                            player.inventory[q].stack--;
                            if (player.inventory[q].stack <= 0)
                            {
                                player.inventory[q] = new Item();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
        return true;
    }
    public static int GemToTile(int type)
    {
        if (type == ItemID.Amethyst) return TileID.Amethyst;
        else if (type == ItemID.Diamond) return TileID.Diamond;
        else if (type == ItemID.Emerald) return TileID.Emerald;
        else if (type == ModContent.ItemType<Kunzite>()) return ModContent.TileType<Tiles.Ores.Kunzite>();
        else if (type == ModContent.ItemType<Onyx>()) return ModContent.TileType<Tiles.Ores.Onyx>();
        else if (type == ModContent.ItemType<Opal>()) return ModContent.TileType<Tiles.Ores.Opal>();
        else if (type == ModContent.ItemType<Peridot>()) return ModContent.TileType<Tiles.Ores.Peridot>();
        else if (type == ItemID.Ruby) return TileID.Ruby;
        else if (type == ItemID.Sapphire) return TileID.Sapphire;
        else if (type == ItemID.Topaz) return TileID.Topaz;
        else if (type == ModContent.ItemType<Tourmaline>()) return ModContent.TileType<Tiles.Ores.Tourmaline>();
        else if (type == ModContent.ItemType<Zircon>()) return ModContent.TileType<Tiles.Ores.Zircon>();
        return 0;
    }
}
