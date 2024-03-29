using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Weapons.Magic;

class FrozenLyre : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Frozen Lyre");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        //Item.UseSound = SoundID.Item26;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 16;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.shootSpeed = 6f;
        Item.mana = 4;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = 20;
        Item.knockBack = 0.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.IceNote>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.holdStyle = 3;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.useAnimation = 20;
    }
    public SoundStyle note = new SoundStyle("Terraria/Sounds/Item_26")
    {
        Volume = 1f,
        Pitch = 0f,
        PitchVariance = 0.5f,
        MaxInstances = 10,
    };
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        SoundEngine.PlaySound(note, player.Center);
        return true;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6, 0);
    }
}
