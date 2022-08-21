using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class TritonCanister : ModItem
{
    public override void SetStaticDefaults()
    {
        SacrificeTotal = 99;
    }
    public override void SetDefaults()
    {
        Item.damage = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 14;
        Item.height = 18;
        Item.maxStack = 2000;
        Item.consumable = true;
        Item.value = 10;
        Item.rare = ModContent.RarityType<Rarities.MagentaRarity>();
        Item.ammo = ModContent.ItemType<Canister>();
        Item.shoot = ModContent.ProjectileType<Projectiles.TritonFire>();
    }
}
