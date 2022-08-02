using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Avalon.Items.Weapons.Melee;

public class InsectoidBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Insectoid Blade");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = Item.useAnimation = 24;
        Item.damage = 25;
        Item.knockBack = 2;
        Item.scale = 1.1f;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Orange;
        Item.DamageType = DamageClass.Melee;
        Item.value = Item.sellPrice(0, 0, 54, 0);
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
            int dust = Dust.NewDust(
                new Vector2(hitbox.X, hitbox.Y),
                hitbox.Width,
                hitbox.Height,
                //DustID.Blood,
                ModContent.DustType<Dusts.MosquitoDust>(),
                (player.velocity.X * 0.2f) + (player.direction * 3),
                player.velocity.Y * 0.2f,
                0,
                new Color(),
                1f
            );
            Main.dust[dust].noGravity = true;
        }
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        if (Main.rand.Next(4) == 0)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Malaria>(), 420);
        }
    }
    public override void OnHitPvp(Player player, Player target, int damage, bool crit)
    {
        if (Main.rand.Next(4) == 0)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Malaria>(), 420);
        }
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<TropicalShroomCap>(), 12)
            .AddIngredient(ModContent.ItemType<MosquitoProboscis>(), 12)
            .AddTile(TileID.Anvils).Register();
    }
}
