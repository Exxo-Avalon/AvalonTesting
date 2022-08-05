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
        Tooltip.SetDefault("Fires a burst of 3 arrows\nWooden arrows are converted into Pyroscoric bolts\nPyroscoric bolts explode into fire when all 3 shots hit");
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
        Item.rare = ModContent.RarityType<MagentaRarity>();
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
    private int HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant = 1;
    private float NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool = 1;
    private Vector2 shoothere;
    private Vector2 muzzleOffset = Vector2.One;
    private int HeyLookAtThatThingOverThereJustDontMakeItObviousBro;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant > 2)
        {
            HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant = 1;
            NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool = 1;
        }
        else
        {
            HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant++;
            NoSpecialArrowHowSadDamageModifierThingymadoohickeyRealOnGodSuperCoolAmazingWowieZowieWubzieBubzieSuperCool += 0.4f;
        }
        SoundEngine.PlaySound(SoundID.Item102, player.position);
        return base.Shoot( player,  source,  position,  velocity,  type,  damage,  knockback);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        // check if the first shot
        if (HowManyTimesHasTheFunnyArrowsBeenShotPleaseTellMeItsImportant == 1)
        {
            muzzleOffset = Vector2.Normalize(velocity) * 40f; // assign the velocity to the muzzleOffset field
            shoothere = velocity; // assign the velocity to the shoothere field
            // assign the player's direction to force them to look in the same direction for all shots later on
            HeyLookAtThatThingOverThereJustDontMakeItObviousBro = player.direction;
        }
        // if not the first shot
        else
        {
            velocity = shoothere; // reassign the velocity
            player.direction = HeyLookAtThatThingOverThereJustDontMakeItObviousBro; // reassign the direction
            position = player.MountedCenter + muzzleOffset; // assign the position

            // if the bolts can hit the position plus the muzzle offset, add the muzzle offset to the position
            if (Collision.CanHit(position, 1, 1, position + muzzleOffset, 1, 1))
            {
                position += muzzleOffset;
            }
        }
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
