using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Items.Weapons.Melee;

class CaesiumScimitar : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Caesium Scimitar");
        Tooltip.SetDefault("Explodes foes on hit");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 66;
        Item.useTurn = true;
        Item.scale = 1.3f;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 8f;
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.useAnimation = 18;
        Item.height = dims.Height;
    }
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        SoundEngine.PlaySound(SoundID.Item14, target.position);
        for (int i = 0; i < 5; i++)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y, Main.rand.Next(-2, 3), Main.rand.Next(-2, 3), ModContent.ProjectileType<Projectiles.CaesiumExplosion>(), damage, knockBack, player.whoAmI, 0f, 0f);
        }
        target.AddBuff(BuffID.OnFire, 60 * 5);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CaesiumBar>(), 32)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
