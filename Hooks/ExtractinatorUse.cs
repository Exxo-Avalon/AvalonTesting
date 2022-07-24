using System;
using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameInput;
using Microsoft.Xna.Framework;

namespace Avalon.Hooks
{
    [Autoload(Side = ModSide.Both)]
    public class ExtractinatorUse : ModHook
    {
        protected override void Apply()
        {
			On.Terraria.Player.ExtractinatorUse += OnExtractinatorUse;
        }
        private static void OnExtractinatorUse(On.Terraria.Player.orig_ExtractinatorUse orig, Player self, int extractType)
        {
			int num = 5000;
			int num2 = 25;
			int num3 = 50;
			int num4 = -1;
			if (extractType == 1)
			{
				num /= 3;
				num2 *= 2;
				num3 = 20;
				num4 = 10;
			}
			int itemType = -1;
			int itemStack = 1;
			if (num4 != -1 && Main.rand.NextBool(num4))
			{
				itemType = 3380;
				if (Main.rand.NextBool(5))
				{
					itemStack += Main.rand.Next(2);
				}
				if (Main.rand.NextBool(10))
				{
					itemStack += Main.rand.Next(3);
				}
				if (Main.rand.NextBool(15))
				{
					itemStack += Main.rand.Next(4);
				}
			}
			else if (Main.rand.NextBool(2))
			{
				if (Main.rand.NextBool(12000))
				{
					itemType = ItemID.PlatinumCoin;
					if (Main.rand.NextBool(14))
					{
						itemStack += Main.rand.Next(0, 2);
					}
					if (Main.rand.NextBool(14))
					{
						itemStack += Main.rand.Next(0, 2);
					}
					if (Main.rand.NextBool(14))
					{
						itemStack += Main.rand.Next(0, 2);
					}
				}
				else if (Main.rand.NextBool(800))
				{
					itemType = ItemID.GoldCoin;
					if (Main.rand.NextBool(6))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(6))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(6))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(6))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(6))
					{
						itemStack += Main.rand.Next(1, 20);
					}
				}
				else if (Main.rand.NextBool(60))
				{
					itemType = ItemID.SilverCoin;
					if (Main.rand.NextBool(4))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(4))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(4))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(4))
					{
						itemStack += Main.rand.Next(5, 25);
					}
				}
				else
				{
					itemType = ItemID.CopperCoin;
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(10, 25);
					}
				}
			}
			else if (num != -1 && Main.rand.NextBool(num))
			{
				itemType = ItemID.AmberMosquito;
			}
			else if (num2 != -1 && Main.rand.NextBool(num2))
			{
				switch (Main.rand.Next(9))
				{
					case 0:
						itemType = ItemID.Amethyst;
						break;
					case 1:
						itemType = ItemID.Topaz;
						break;
					case 2:
						itemType = ItemID.Sapphire;
						break;
					case 3:
						itemType = ItemID.Emerald;
						break;
					case 4:
						itemType = ItemID.Ruby;
						break;
					case 5:
						itemType = ItemID.Diamond;
						break;
					case 6:
						itemType = ModContent.ItemType<Items.Placeable.Tile.Tourmaline>();
						break;
					case 7:
						itemType = ModContent.ItemType<Items.Placeable.Tile.Peridot>();
						break;
					case 8:
						itemType = ModContent.ItemType<Items.Material.Zircon>();
						break;
				}
				if (Main.rand.NextBool(20))
				{
					itemStack += Main.rand.Next(0, 2);
				}
				if (Main.rand.NextBool(30))
				{
					itemStack += Main.rand.Next(0, 3);
				}
				if (Main.rand.NextBool(40))
				{
					itemStack += Main.rand.Next(0, 4);
				}
				if (Main.rand.NextBool(50))
				{
					itemStack += Main.rand.Next(0, 5);
				}
				if (Main.rand.NextBool(60))
				{
					itemStack += Main.rand.Next(0, 6);
				}
			}
			else if (num3 != -1 && Main.rand.NextBool(num3))
			{
				itemType = ItemID.Amber;
				if (Main.rand.NextBool(20))
				{
					itemStack += Main.rand.Next(0, 2);
				}
				if (Main.rand.NextBool(30))
				{
					itemStack += Main.rand.Next(0, 3);
				}
				if (Main.rand.NextBool(40))
				{
					itemStack += Main.rand.Next(0, 4);
				}
				if (Main.rand.NextBool(50))
				{
					itemStack += Main.rand.Next(0, 5);
				}
				if (Main.rand.NextBool(60))
				{
					itemStack += Main.rand.Next(0, 6);
				}
			}
			else if (Main.rand.NextBool(3))
			{
				if (Main.rand.NextBool(5000))
				{
					itemType = ItemID.PlatinumCoin;
					if (Main.rand.NextBool(10))
					{
						itemStack += Main.rand.Next(0, 3);
					}
					if (Main.rand.NextBool(10))
					{
						itemStack += Main.rand.Next(0, 3);
					}
					if (Main.rand.NextBool(10))
					{
						itemStack += Main.rand.Next(0, 3);
					}
					if (Main.rand.NextBool(10))
					{
						itemStack += Main.rand.Next(0, 3);
					}
					if (Main.rand.NextBool(10))
					{
						itemStack += Main.rand.Next(0, 3);
					}
				}
				else if (Main.rand.NextBool(400))
				{
					itemType = ItemID.GoldCoin;
					if (Main.rand.NextBool(5))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(5))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(5))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(5))
					{
						itemStack += Main.rand.Next(1, 21);
					}
					if (Main.rand.NextBool(5))
					{
						itemStack += Main.rand.Next(1, 20);
					}
				}
				else if (Main.rand.NextBool(30))
				{
					itemType = ItemID.SilverCoin;
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(5, 26);
					}
					if (Main.rand.NextBool(3))
					{
						itemStack += Main.rand.Next(5, 25);
					}
				}
				else
				{
					itemType = ItemID.CopperCoin;
					if (Main.rand.NextBool(2))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(2))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(2))
					{
						itemStack += Main.rand.Next(10, 26);
					}
					if (Main.rand.NextBool(2))
					{
						itemStack += Main.rand.Next(10, 25);
					}
				}
			}
			else
			{
				switch (Main.rand.Next(18))
				{
					case 0:
						itemType = ItemID.CopperOre;
						break;
					case 1:
						itemType = ItemID.IronOre;
						break;
					case 2:
						itemType = ItemID.SilverOre;
						break;
					case 3:
						itemType = ItemID.GoldOre;
						break;
					case 4:
						itemType = ItemID.TinOre;
						break;
					case 5:
						itemType = ItemID.LeadOre;
						break;
					case 6:
						itemType = ItemID.TungstenOre;
						break;
					case 7:
						itemType = ItemID.PlatinumOre;
						break;
					case 8:
						itemType = ModContent.ItemType<Items.Placeable.Tile.BronzeOre>();
						break;
					case 9:
						itemType = ModContent.ItemType<Items.Placeable.Tile.NickelOre>();
						break;
					case 10:
						itemType = ModContent.ItemType<Items.Placeable.Tile.ZincOre>();
						break;
					case 11:
						itemType = ModContent.ItemType<Items.Placeable.Tile.BismuthOre>();
						break;
					case 12:
						itemType = ModContent.ItemType<Items.Placeable.Tile.Heartstone>();
						break;
					case 13:
						itemType = ModContent.ItemType<Items.Placeable.Tile.Boltstone>();
						break;
					case 14:
						itemType = ModContent.ItemType<Items.Placeable.Tile.Starstone>();
						break;
					case 15:
						itemType = ModContent.ItemType<Items.Placeable.Tile.RhodiumOre>();
						break;
					case 16:
						itemType = ModContent.ItemType<Items.Placeable.Tile.OsmiumOre>();
						break;
					case 17:
						itemType = ModContent.ItemType<Items.Placeable.Tile.IridiumOre>();
						break;
				}
				if (Main.rand.NextBool(20))
				{
					itemStack += Main.rand.Next(0, 2);
				}
				if (Main.rand.NextBool(30))
				{
					itemStack += Main.rand.Next(0, 3);
				}
				if (Main.rand.NextBool(40))
				{
					itemStack += Main.rand.Next(0, 4);
				}
				if (Main.rand.NextBool(50))
				{
					itemStack += Main.rand.Next(0, 5);
				}
				if (Main.rand.NextBool(60))
				{
					itemStack += Main.rand.Next(0, 6);
				}
			}
			if (itemType > 0)
			{
				Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen) + Main.screenPosition;
				if (Main.SmartCursorIsUsed || PlayerInput.UsingGamepad)
				{
					vector = self.Center;
				}
				int number = Item.NewItem(self.GetSource_DropAsItem(), (int)vector.X, (int)vector.Y, 1, 1, itemType, itemStack, noBroadcast: false, -1);
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f);
				}
			}
		}
    }
}
