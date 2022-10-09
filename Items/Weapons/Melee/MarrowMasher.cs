using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Items.Weapons.Melee;

class MarrowMasher : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Marrow Masher");
        Tooltip.SetDefault("Critical strikes have increased knockback");
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 35;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.crit = 6;
        Item.shootSpeed = 6f; //so the knockback works properly
        Item.rare = ItemRarityID.Green;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = Item.useAnimation = 40;
        Item.knockBack = 6.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.MarrowMasher>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 40, 0);
    }
    public int swing;
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (swing == 1)
        {
            swing--;
            position = player.Center + new Vector2(0, 85f);
        }
        else
        {
            swing++;
            position = player.Center + new Vector2(0, -85f);
        }
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
