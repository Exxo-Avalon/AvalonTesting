
using Avalon.Projectiles.Summon;
using Avalon.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon;
public class Skylash : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<SkylashProjectile>(), 40, 2, 4,25); //WTF THESE EXIST????
        Item.shootSpeed = 4;
        Item.rare = ModContent.RarityType<CrabbyRarity>();
        Item.autoReuse = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddIngredient(ItemID.Feather, 6)
            .AddIngredient(ItemID.SoulofFlight, 5)
            .AddIngredient(ModContent.ItemType<Placeable.Bar.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool MeleePrefix() => true;
}
