using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged;

class PyroscoricRepeater : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Pyroscoric Repeater");
        Tooltip.SetDefault("Fires a burst of 3 arrows\nWooden arrows are converted into pyroscoric bolts.\nPyroscoric bolts explode into fire when all 3 shots hit");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 150;
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
        Item.consumeAmmoOnFirstShotOnly = true;
        Item.reuseDelay = 15;
        Item.useAnimation = 36;
        Item.useTime = 12;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10f, 0f);
    }
    public int HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant = 1;
    public float NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool = 1;
    public Vector2 shoothere;
    public int HeyLookAtThatThingOverThereJustDontMakeItObviousBro = 0;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant > 2)
        {
            HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant = 1; NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool = 1;
        }
        else
        { HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant++; NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool += 0.4f;}
        SoundEngine.PlaySound(SoundID.Item102, player.position);
        return base.Shoot( player,  source,  position,  velocity,  type,  damage,  knockback);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
        /* //broken thing for making all 3 shots go in the same directions
        if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
        {
            position += muzzleOffset;
        }

            if (HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant == 1)
        {
            shoothere = velocity;
            HeyLookAtThatThingOverThereJustDontMakeItObviousBro = player.direction;
        }
        velocity = shoothere;
        player.direction = HeyLookAtThatThingOverThereJustDontMakeItObviousBro;
        position = player.MountedCenter + muzzleOffset;
        */
        if (type == ProjectileID.WoodenArrowFriendly)
        {
            type = ModContent.ProjectileType<Projectiles.Ranged.PyroBolt>();
        }
        else
        {
            damage = (int)(damage * NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool);
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.PyroscoricBar>(), 21)
            .AddTile(TileID.MythrilAnvil).Register();
    }
}
