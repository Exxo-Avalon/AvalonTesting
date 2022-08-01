using Avalon.Buffs;
using Avalon.Items.Placeable.Bar;
using Avalon.PlayerDrawLayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon;

public class TroxiniumDaggerStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Troxinium Dagger Staff");
        Tooltip.SetDefault("Summons a troxinium dagger to fight for you");
        SacrificeTotal = 1;
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = false;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;

        Item.damage = 28;
        Item.mana = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.useTime = 30;
        Item.knockBack = 5.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 3, 20);
        Item.useAnimation = 30;
        Item.UseSound = SoundID.Item44;

        Item.DamageType = DamageClass.Summon;
        Item.noMelee = true;
        Item.buffType = ModContent.BuffType<TroxiniumDagger>();
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.TroxiniumDagger>();
        if (!Main.dedServ)
        {
            Item.GetGlobalItem<ItemGlowmask>().glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        }
    }
    public override bool CanUseItem(Player player)
    {
        return (player.ownedProjectileCounts[Item.shoot] < player.maxMinions);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
                               int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
        return false;
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
        CreateRecipe().AddIngredient(ModContent.ItemType<TroxiniumBar>(), 22).AddTile(TileID.Anvils).Register();
    }
}
