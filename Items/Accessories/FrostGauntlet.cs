using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories;

[AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
class FrostGauntlet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Frost Gauntlet");
        Tooltip.SetDefault("Melee attacks inflict Frostburn and increases damage and melee speed by 9%\nIncreases knockback and puts a damage-reducing shell around the holder when below 50% life\nEnables auto swing for melee weapons and increases the size of melee weapons");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.FireGauntlet)
            .AddIngredient(ItemID.FrozenTurtleShell)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (player.statLife <= player.statLifeMax2 * 0.5)
        {
            player.AddBuff(62, 5, true);
        }
        player.GetModPlayer<Players.ExxoEquipEffectPlayer>().FrostGauntlet = true;
        player.kbGlove = true;
        player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.frostArmor = true;
        player.autoReuseGlove = true;
        player.meleeScaleGlove = true;
    }
}
