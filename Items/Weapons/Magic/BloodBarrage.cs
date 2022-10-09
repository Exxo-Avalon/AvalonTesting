using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic;

class BloodBarrage : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blood Barrage");
        Tooltip.SetDefault("Uses 4 life\nReturns life on hit");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 30;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 12f;
        Item.mana = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 8;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloodBlob>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 50000;
        Item.useAnimation = 24;
        Item.reuseDelay = 20;
        Item.height = dims.Height;
    }
    public override bool? UseItem(Player player)
    {
        int healthSucked = 4;
        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
        player.statLife -= healthSucked;
        return true;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity = velocity.RotatedByRandom(0.17);
    }
}
