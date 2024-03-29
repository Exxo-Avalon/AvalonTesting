using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ore;

class TroxiniumOre : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Ore");
        SacrificeTotal = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.Ores.TroxiniumOre>();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 15, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    //{
    //    Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
    //    spriteBatch.Draw
    //    (
    //        texture,
    //        new Vector2
    //        (
    //            Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
    //            Item.position.Y - Main.screenPosition.Y + Item.height * 0.5f
    //        ),
    //        new Rectangle(0, 0, texture.Width, texture.Height),
    //        Color.White,
    //        rotation,
    //        texture.Size() * 0.5f,
    //        scale,
    //        SpriteEffects.None,
    //        0f
    //    );
    //}
}
