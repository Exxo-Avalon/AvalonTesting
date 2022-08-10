using System;
using Avalon.Items.Placeable.Bar;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

public class BerserkerBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Berserker Blade");
        Tooltip.SetDefault("'Go berserk!'");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 166;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.rare = ModContent.RarityType<Rarities.DarkRedRarity>();
        Item.width = 54;
        Item.height = 50;
        Item.useTime = 10;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 12);
        Item.useAnimation = 10;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        //Old glowmask code, shit doesn't work properly, uncommented is adapted vanilla code which works
        //Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        //spriteBatch.Draw
        //(
        //    texture,
        //    new Vector2
        //    (
        //        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
        //        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
        //    ),
        //    new Rectangle(0, 0, texture.Width, texture.Height),
        //    Color.White,
        //    rotation,
        //    texture.Size() * 0.5f,
        //    scale,
        //    SpriteEffects.None,
        //    0f
        //);
        Rectangle dims = this.GetDims();
        Vector2 vector = dims.Size() / 2f;
        Vector2 value = new Vector2((float)(Item.width / 2) - vector.X, Item.height - dims.Height);
        Vector2 vector2 = Item.position - Main.screenPosition + vector + value;
        float num = Item.velocity.X * 0.2f;
        spriteBatch.Draw((Texture2D)ModContent.Request<Texture2D>(Texture + "_Glow"), vector2, dims, new Color(250, 250, 250, 250), num, vector, scale, SpriteEffects.None, 0f);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BerserkerBar>(), 40).AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 20).AddIngredient(ModContent.ItemType<Material.ElementShard>(), 5).AddIngredient(ModContent.ItemType<Material.VictoryPiece>()).AddIngredient(ModContent.ItemType<VoraylzumKatana>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BerserkerBar>(), 40).AddIngredient(ModContent.ItemType<Material.SoulofTorture>(), 20).AddIngredient(ModContent.ItemType<Material.ElementShard>(), 5).AddIngredient(ModContent.ItemType<Material.VictoryPiece>()).AddIngredient(ModContent.ItemType<UnvolanditeGreatsword>()).AddTile(ModContent.TileType<Tiles.SolariumAnvil>()).Register();
    }
}
