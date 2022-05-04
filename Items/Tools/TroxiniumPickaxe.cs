﻿using AvalonTesting.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Tools;

public class TroxiniumPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Pickaxe");
        Tooltip.SetDefault("Can mine Ferozium");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 25;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.pick = 185;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 8;
        Item.knockBack = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 28, 0);
        Item.useAnimation = 25;
        Item.height = dims.Height;
        //if (!Main.dedServ)
        //{
        //    Item.GetGlobalItem<ItemUseGlow>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        //}
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Texture2D texture = Mod.Assets.Request<Texture2D>("Items/Tools/TroxiniumPickaxe_Glow").Value;
        spriteBatch.Draw
        (
            texture,
            new Vector2
            (
                Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
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
}
