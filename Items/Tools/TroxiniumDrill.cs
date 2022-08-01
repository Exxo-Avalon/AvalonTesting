using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class TroxiniumDrill : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Drill");
        Tooltip.SetDefault("Can mine Ferozium");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 14;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.shootSpeed = 32f;
        Item.pick = 185;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 13;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.TroxiniumDrill>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 114000;
        Item.useAnimation = 25;
        Item.height = dims.Height;
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        spriteBatch.Draw
        (
            texture,
            new Vector2
            (
                Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                Item.position.Y - Main.screenPosition.Y + Item.height * 0.5f
            ),
            new Rectangle(0, 0, texture.Width, texture.Height),
            Color.White,
            rotation,
            texture.Size() * 0.5f,
            scale,
            SpriteEffects.None,
            0f
        );
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.TroxiniumBar>(), 18)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
