using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

public class Terraspin : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Terraspin");
        Tooltip.SetDefault("Fires a spread of typhoons");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item84;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 185;
        Item.autoReuse = true;
        Item.channel = true;
        Item.useTurn = false;
        Item.shootSpeed = 9f;
        Item.crit += 9;
        Item.mana = 26;
        Item.noMelee = true;
        Item.rare = ModContent.RarityType<Rarities.FireOrangeRarity>();
        Item.width = dims.Width;
        Item.knockBack = 7f;
        Item.useTime = 30;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.TerraTyphoon>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 50, 0, 0);
        Item.useAnimation = 30;
        Item.height = dims.Height;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
        Item.GetGlobalItem<ItemGlowmask>().glowOffsetX = 2;
        Item.GetGlobalItem<ItemGlowmask>().glowOffsetY = 0;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(2, 0);
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(Texture + "_Glow"), vector2, dims, new Color(250, 250, 250, 250), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        for (int num212 = 0; num212 < 2; num212++)
        {
            float num213 = velocity.X;
            float num214 = velocity.Y;
            num213 += Main.rand.Next(-30, 31) * 0.05f;
            num214 += Main.rand.Next(-30, 31) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num213, num214, type, damage, knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<DevilsScythe>()).AddIngredient(ModContent.ItemType<TheGoldenFlames>()).AddIngredient(ItemID.RazorbladeTyphoon).AddIngredient(ModContent.ItemType<Material.BrokenVigilanteTome>()).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 5).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
