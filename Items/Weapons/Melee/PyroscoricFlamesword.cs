using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Avalon.Items.Weapons.Melee;

class PyroscoricFlamesword : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Flamesword");
        Tooltip.SetDefault("Shoots a wave of fire\n'It burns, I tell you!'");
        SacrificeTotal = 1;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 131;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.3f;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.width = dims.Width;
        Item.useTime = 25;
        Item.useAnimation = 20;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.FireWave>();
        Item.shootSpeed = 25f;
        Item.value = Item.sellPrice(0, 7, 63, 0);
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(SoundID.Item20 with { Volume = 0.8f, Pitch = 0.25f }, player.position);
        return true;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(2) == 0)
        {
            int num162 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 0, default(Color), 2f);
            Main.dust[num162].noGravity = true;
        }
    }
}
