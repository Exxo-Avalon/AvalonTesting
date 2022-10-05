using Avalon.Items.Material;
using Avalon.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class TheWorld : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The World");
        Tooltip.SetDefault("Tome\n35% increased damage, 20% increased critical strike chance, -25% mana cost\n30% chance to not consume ammo, 18 defense, +260 mana, +160 HP, +120 stamina");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.AvalonRarity>();
        Item.width = dims.Width;
        Item.value = 250000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.35f;
        player.GetCritChance(DamageClass.Melee) += 20;
        player.GetCritChance(DamageClass.Magic) += 20;
        player.GetCritChance(DamageClass.Ranged) += 20;
        player.GetCritChance(DamageClass.Throwing) += 20;
        player.manaCost -= 0.25f;
        player.statDefense += 18;
        player.statLifeMax2 += 160;
        player.statManaMax2 += 260;
        player.GetModPlayer<ExxoStaminaPlayer>().StatStamMax2 += 120;
    }

    public override void AddRecipes()
    {
        // TODO: implement post-UB drops
        //CreateRecipe(1).AddIngredient(ModContent.ItemType<Dominance>()).AddIngredient(ModContent.ItemType<VictoryPiece>(), 3).AddIngredient(ModContent.ItemType<SoulofTorture>(), 25).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    }
}
