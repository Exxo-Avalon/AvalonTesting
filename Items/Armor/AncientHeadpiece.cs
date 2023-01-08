using Avalon.Items.Material;
using Avalon.Players;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor;

[AutoloadEquip(EquipType.Head)]
internal class AncientHeadpiece : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ancient Headpiece");
        Tooltip.SetDefault("23% increased damage\n6% increased critical strike chance");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 30;
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 50);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ItemID.SolarFlareHelmet).AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10).AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5).AddIngredient(ModContent.ItemType<GhostintheMachine>())
            .AddTile(ModContent.TileType<CaesiumForge>()).Register();
        CreateRecipe().AddIngredient(ItemID.NebulaHelmet).AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10).AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5).AddIngredient(ModContent.ItemType<GhostintheMachine>())
            .AddTile(ModContent.TileType<CaesiumForge>()).Register();
        CreateRecipe().AddIngredient(ItemID.StardustHelmet).AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10).AddIngredient(ItemID.FragmentVortex, 10)
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5).AddIngredient(ModContent.ItemType<GhostintheMachine>())
            .AddTile(ModContent.TileType<CaesiumForge>()).Register();
        CreateRecipe().AddIngredient(ItemID.VortexHelmet).AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentStardust, 10).AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ModContent.ItemType<LifeDew>(), 5).AddIngredient(ModContent.ItemType<GhostintheMachine>())
            .AddTile(ModContent.TileType<CaesiumForge>()).Register();
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AncientBodyplate>() &&
               legs.type == ModContent.ItemType<AncientLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        ExxoEquipEffectPlayer modPlayer = player.GetModPlayer<ExxoEquipEffectPlayer>();
        player.setBonus = "Ancient costs 50% less mana"
                          + "\nDouble tap " + Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN")
                                + " to activate Ranger's Wrath status"
                          + "\nTrue melee hits have a chance to grant you a short period of invincibility"
                          + "\nRight-click and hold while holding a summon weapon to direct your minions";
        modPlayer.AncientLessCost = true;
        modPlayer.AncientRangedBonus = true;
        modPlayer.AncientMinionGuide = true;
        modPlayer.AncientSandVortex = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.23f;
        player.GetCritChance(DamageClass.Generic) += 6;
    }
}
