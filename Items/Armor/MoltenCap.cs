using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Armor;

[AutoloadEquip(EquipType.Head)]
class MoltenCap : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Molten Cap");
        Tooltip.SetDefault("Summoner helmet");
        Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 3;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = 30000;
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ItemID.MoltenBreastplate && legs.type == ItemID.MoltenGreaves;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.HellstoneBar, 10).AddIngredient(ModContent.ItemType<Items.Material.FireShard>()).AddTile(TileID.Anvils).Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "17% increased minion damage\nIncreases your max number of minions by 1";
        player.GetDamage(DamageClass.Summon) += 0.17f;
        player.maxMinions++;
        player.buffImmune[BuffID.OnFire] = true;
    }
}
