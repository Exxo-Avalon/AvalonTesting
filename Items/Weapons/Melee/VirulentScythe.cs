using Avalon.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

internal class VirulentScythe : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Virulent Scythe");
        Tooltip.SetDefault("Shoots a scythe that lashes out at enemies\nInflicts Virulent on your enemies");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 46;
        Item.autoReuse = true;
        Item.shootSpeed = 14f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.VirulentScythe>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 20);
        Item.useAnimation = 18;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item39;
    }

    //public override bool CanUseItem(Player player)
    //{
    //    return player.ownedProjectileCounts[Item.shoot] < 6;
    //}
}
