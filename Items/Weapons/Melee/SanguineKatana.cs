using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee
{
    public class SanguineKatana : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Katana");
            Tooltip.SetDefault("Uses 1 life \nReturns life on hit");
        }
        public override void SetDefaults()
        {
            Rectangle dims = this.GetDims();
            Item.damage = 24;
            Item.scale = 1.3f;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Orange;
            Item.width = dims.Width;
            Item.height = dims.Height;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if(target.type != NPCID.TargetDummy)
            {
                player.HealEffect(2, true);
                player.statLife += 2;
            }
        }
        public override bool? UseItem(Player player)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, -1, dramatic: false, dot: false);
            player.statLife--;
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int num209 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
                Dust dust = Main.dust[num209];
                dust.velocity.X = 2f * player.direction;
                dust.velocity.Y = -0.5f;
            }
        }
    }
}

