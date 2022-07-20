using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace AvalonTesting.Items.Tools
{
	
	public class TombMirror : ModItem
	{
		//TODO Add a recipe and improve visuals if you desire

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tomb Mirror");
			Tooltip.SetDefault("Use tor return to where you last died \n'Gazing into the mirror you see yourself in a past life'");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.maxStack = 1;
			Item.consumable = true;
			Item.width = 20;
			Item.height = 22;
			Item.rare = ItemRarityID.LightRed;
			Item.useStyle = ItemHoldStyleID.HoldUp;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.consumable = false;
			Item.UseSound = SoundID.Item6;
			Item.autoReuse = false;
		}

		public override bool? UseItem(Player player)
        {



			if (player.GetModPlayer<SaveDeathpoint>().DeathX == 0 && player.GetModPlayer<SaveDeathpoint>().DeathY == 0)
            {
				return false;

			}
            else 
			{
				
				player.Center = new Vector2(player.GetModPlayer<SaveDeathpoint>().DeathX, player.GetModPlayer<SaveDeathpoint>().DeathY);

				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(player.position, player.width, player.height, 6);
				}
				return true;
			}
        }


    }

	public class SaveDeathpoint : ModPlayer
	{

		public float DeathX;
		public float DeathY;
		public int Clock;

        public override void UpdateDead()
        {
			if (Clock == 0)
            {
				DeathX = Player.Center.X;
				DeathY = Player.Center.Y;
				Clock = 1;
			}
		}

        public override void OnRespawn(Player player)
        {
			Clock = 0;
		}

    }



}
