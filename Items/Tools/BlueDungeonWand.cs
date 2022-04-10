using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

class BlueDungeonWand : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blue Dungeon Wand");
        Tooltip.SetDefault("Places unsafe blue dungeon walls");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.BlueBrick, 50).AddIngredient(ItemID.GoldenKey, 2).AddIngredient(ItemID.Bone, 20).AddTile(TileID.BoneWelder).Register();
    }
    public override bool? UseItem(Player player)
    {
        if (Main.myPlayer == player.whoAmI)
        {
            for (int q = 0; q < player.inventory.Length; q++)
            {
                int type = player.inventory[q].type;
                bool inrange = (player.position.X / 16f - Player.tileRangeX - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetX &&
                                (player.position.X + player.width) / 16f + Player.tileRangeX + player.inventory[player.selectedItem].tileBoost - 1f + player.blockRange >= Player.tileTargetX &&
                                player.position.Y / 16f - Player.tileRangeY - player.inventory[player.selectedItem].tileBoost - player.blockRange <= Player.tileTargetY &&
                                (player.position.Y + player.height) / 16f + Player.tileRangeY + player.inventory[player.selectedItem].tileBoost - 2f + player.blockRange >= Player.tileTargetY);
                if (type == ItemID.BlueTiledWall || type == ItemID.BlueBrickWall || type == ItemID.BlueSlabWall)
                {
                    if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType == 0 && inrange)
                    {
                        WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, DungeonWallItemToBackwallID(type));
                        if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType != 0 && Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 3, Player.tileTargetX, Player.tileTargetY, DungeonWallItemToBackwallID(type));
                        }
                        player.inventory[q].stack--;
                        if (player.inventory[q].stack <= 0)
                        {
                            player.inventory[q] = new Item();
                            break;
                        }
                        break;
                    }
                }
            }
        }
        return true;
    }
    public static int DungeonWallItemToBackwallID(int type)
    {
        if (type == ItemID.BlueBrickWall) return WallID.BlueDungeonUnsafe;
        else if (type == ItemID.BlueSlabWall) return WallID.BlueDungeonSlabUnsafe;
        else if (type == ItemID.BlueTiledWall) return WallID.BlueDungeonTileUnsafe;
        else if (type == ItemID.GreenBrickWall) return WallID.GreenDungeonUnsafe;
        else if (type == ItemID.GreenSlabWall) return WallID.GreenDungeonSlabUnsafe;
        else if (type == ItemID.GreenTiledWall) return WallID.GreenDungeonTileUnsafe;
        else if (type == ItemID.PinkBrickWall) return WallID.PinkDungeonUnsafe;
        else if (type == ItemID.PinkSlabWall) return WallID.PinkDungeonSlabUnsafe;
        else if (type == ItemID.PinkTiledWall) return WallID.PinkDungeonTileUnsafe;
        else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeBrickWall>()) return ModContent.WallType<Walls.OrangeBrickUnsafe>();
        else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeSlabWall>()) return ModContent.WallType<Walls.OrangeSlabUnsafe>();
        else if (type == ModContent.ItemType<Items.Placeable.Wall.OrangeTiledWall>()) return ModContent.WallType<Walls.OrangeTiledUnsafe>();
        return 0;
    }
}
