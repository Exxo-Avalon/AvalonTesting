using AvalonTesting.Items.Material;
using AvalonTesting.Items.Placeable.Bar;
using AvalonTesting.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
internal class CorruptedThornCrown : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Corrupted Thorn Crown");
        Tooltip.SetDefault("35% increased magic damage");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 2, 10);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Spike, 20)
            .AddIngredient(ModContent.ItemType<CorruptedBar>(), 20)
            .AddIngredient(ModContent.ItemType<CaesiumBar>(), 15)
            .AddIngredient(ItemID.SoulofNight, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<CorruptedThornBodyarmor>() &&
               legs.type == ModContent.ItemType<CorruptedThornGreaves>();
    }

    public override void UpdateArmorSet(Player player)
    {
        ExxoPlayer modPlayer = player.Avalon();
        player.setBonus = "Blood Casting, Necrotic Aura, 75% increased mana usage";
        modPlayer.bloodCast = true;
        modPlayer.necroticAura = true;
        player.manaCost += 0.75f;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.35f;
    }
}
