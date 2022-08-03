using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

class TerraClaws : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Terra Claws");
        Tooltip.SetDefault("Increases melee damage and speed by 14%\nMelee attacks will burn, poison, envenom, frostbite, or ichor your enemies\nEnables auto swing for melee weapons and increases the size of melee weapons");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.RainbowRarity>();
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 15, 0, 0);
        Item.height = dims.Height;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ExxoEquipEffectPlayer>().TerraClaws = true;
        player.GetDamage(DamageClass.Melee) += 0.14f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.14f;
        player.autoReuseGlove = true;
        player.meleeScaleGlove = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.FireGauntlet)
            .AddIngredient(ItemID.Ichor, 20)
            .AddIngredient(ItemID.CursedFlame, 20)
            .AddIngredient(ItemID.SpiderFang, 20)
            .AddIngredient(ItemID.Stinger, 20)
            .AddIngredient(ModContent.ItemType<Material.FrostShard>(), 20)
            .AddIngredient(ModContent.ItemType<Material.Pathogen>(), 20)
            .AddIngredient(ModContent.ItemType<Material.LifeDew>(), 5)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
}
