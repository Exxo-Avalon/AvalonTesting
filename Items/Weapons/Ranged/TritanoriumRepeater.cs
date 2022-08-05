using Avalon.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class TritanoriumRepeater : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Tritanorium Repeater");
        Tooltip.SetDefault("Becomes more accurate and the longer you fire\nTaking the damage will throw off your aim.");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 50;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Arrow;
        Item.shootSpeed = 15f;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ModContent.RarityType<FireOrangeRarity>();
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 7f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item5;
        Item.consumeAmmoOnFirstShotOnly = true;
        Item.useAnimation = 6;
        Item.useTime = 6;
        Item.channel = true;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10f, 0f);
    }
    public int HowManyBulletsHasBeenShotten;

    public override void HoldItem(Player player)
    {
        if (!player.channel)
        {
            player.GetModPlayer<TritanoriumRepeaterModPlayer>().BudgetRecoil = MathHelper.Pi / 6;
        }
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (player.channel)
        {
            velocity = velocity.RotatedByRandom(player.GetModPlayer<TritanoriumRepeaterModPlayer>().BudgetRecoil) * Main.rand.NextFloat(0.9f,1.1f);
            if (player.GetModPlayer<TritanoriumRepeaterModPlayer>().BudgetRecoil > 0)
            {
                player.GetModPlayer<TritanoriumRepeaterModPlayer>().BudgetRecoil -= 0.005f;
            }
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.TritanoriumBar>(), 20)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}

public class TritanoriumRepeaterModPlayer : ModPlayer
{
    public float BudgetRecoil;
    public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
    {
        BudgetRecoil = MathHelper.Pi / 6;
        return base.PreHurt(pvp,quiet,ref damage,ref hitDirection,ref crit,ref customDamage, ref playSound,ref genGore,ref damageSource,ref cooldownCounter);
    }
}
