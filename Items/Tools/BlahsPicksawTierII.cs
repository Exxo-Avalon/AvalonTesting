using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class BlahsPicksawTierII : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blah's Picksaw (Tier II)");
        Tooltip.SetDefault("The user can mine at warp speed");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 55;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.15f;
        Item.axe = 60;
        Item.pick = 700;
        Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
        Item.width = dims.Width;
        Item.useTime = 6;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost += 8;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 5016000;
        Item.useAnimation = 6;
        Item.height = dims.Height;
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Item.type)
        {
            player.pickSpeed -= 0.75f;
        }
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
