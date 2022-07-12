using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Ammo;

public class TritonCanister : ModItem
{
    public override void SetStaticDefaults()
    {
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }
    public override void SetDefaults()
    {
        Item.damage = 16;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 14;
        Item.height = 18;
        Item.maxStack = 999;
        Item.consumable = true;
        Item.value = 10;
        Item.rare = ItemRarityID.Red;
        Item.ammo = ModContent.ItemType<Canister>();
        Item.shoot = ModContent.ProjectileType<Projectiles.TritonFire>();
    }
}
